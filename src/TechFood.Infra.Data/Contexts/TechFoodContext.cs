using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFood.Domain.Entities;
using TechFood.Domain.Shared.Entities;
using TechFood.Infra.Data.Mappings;

namespace TechFood.Infra.Data.Contexts
{
    public class TechFoodContext(
        IHttpContextAccessor httpContextAccessor,
        DbContextOptions<TechFoodContext> options) : DbContext(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<PaymentType> PaymentTypes { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public async Task CommitChangesAsync()
        {
            // get hold of all the domain events
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(entry => entry.Entity.PopDomainEvents())
                .SelectMany(x => x)
                .ToList();

            if (domainEvents.Count != 0)
            {
                // store them in the http context for later
                AddDomainEventsToOfflineProcessingQueue(domainEvents);
            }

            await SaveChangesAsync();
        }

        private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
        {
            // fetch queue from http context or create a new queue if it doesn't exist
            var eventsQueue = _httpContextAccessor.HttpContext!
                .Items
                .TryGetValue(Application.Common.EventualConsistency.Mediator.EventsQueueKey, out var value) && value is Queue<INotification> existingEvents
                    ? existingEvents
                    : new Queue<INotification>();

            // add the domain events to the end of the queue
            domainEvents.ForEach(eventsQueue.Enqueue);

            // store the queue in the http context
            _httpContextAccessor.HttpContext.Items[Application.Common.EventualConsistency.Mediator.EventsQueueKey] = eventsQueue;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PaymentTypeMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            var properties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties());

            var stringProperties = properties.Where(p => p.ClrType == typeof(string));
            foreach (var property in stringProperties)
            {
                var maxLength = property.GetMaxLength() ?? 50;

                property.SetColumnType($"varchar({maxLength})");
            }

            var booleanProperties = properties
                .Where(p => p.ClrType == typeof(bool) ||
                            p.ClrType == typeof(bool?));
            foreach (var property in booleanProperties)
            {
                property.SetColumnType("bit");
                property.IsNullable = false;
            }

            var dateTimeProperties = properties.Where(p => p.ClrType == typeof(DateTime));
            foreach (var property in dateTimeProperties)
            {
                property.SetColumnType("datetime");
            }

            var enumProperties = properties.Where(p => p.ClrType == typeof(Enum));
            foreach (var property in enumProperties)
            {
                property.SetColumnType("smallint");
            }

            SeedContext(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(
                    new { Id = 1, Name = "Mercado", ImageFileName = "Mercado-nov_20_v3gC.jpg" },
                    new { Id = 2, Name = "Farmácia", ImageFileName = "farmacia_eTzH.png" },
                    new { Id = 3, Name = "Lanches", ImageFileName = "Lanches-out_20_ICvT.jpg" },
                    new { Id = 4, Name = "Pizza", ImageFileName = "Pizza-out_20_YJkb.jpg" },
                    new { Id = 5, Name = "Vegetariana", ImageFileName = "Vegetarianaout20_Zdx4.png" },
                    new { Id = 6, Name = "Árabe", ImageFileName = "Arabe-out_20_b13G.jpg" },
                    new { Id = 7, Name = "Japonesa", ImageFileName = "Japonesa-out_20_Y35F.jpg" },
                    new { Id = 8, Name = "Bebidas", ImageFileName = "5fc6b196-73a3-4d48-a4ab-3bf0e747c101_IGuo.png" },
                    new { Id = 9, Name = "Marmita", ImageFileName = "Marmita-nov_20_Jdve.jpg" },
                    new { Id = 10, Name = "Doces & Bolos", ImageFileName = "Doces_e_bolos-out_20_AJ6s.jpg" }
                );

            modelBuilder.Entity<PaymentType>()
                .HasData(
                    new { Id = 1, Code = "MCMA", Description = "Mastercard" },
                    new { Id = 2, Code = "VIS", Description = "Visa" },
                    new { Id = 3, Code = "ELO", Description = "Elo" },
                    new { Id = 4, Code = "DNR", Description = "Sodexo" },
                    new { Id = 5, Code = "VR", Description = "Vale Refeição" },
                    new { Id = 6, Code = "PIX", Description = "Pix" }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }
    }
}
