using System.Threading.Tasks;
using TechFood.Domain.UoW;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    public class AnotherUnitOfWork : IUnitOfWork
    {
        private readonly TechFoodContext _context;

        public AnotherUnitOfWork(TechFoodContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> CommitAsync()
        {
            var success = (await _context.SaveChangesAsync()) > 0;
            return success;
        }

        public Task RollbackAsync()
        {
            return Task.CompletedTask;
        }
    }
}
