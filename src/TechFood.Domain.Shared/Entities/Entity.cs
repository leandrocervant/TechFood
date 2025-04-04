using System.Collections.Generic;
using System.Linq;

namespace TechFood.Domain.Shared.Entities
{
    public class Entity
    {
        public int Id { get; set; }

        protected readonly List<IDomainEvent> _domainEvents = [];

        public List<IDomainEvent> PopDomainEvents()
        {
            var copy = _domainEvents.ToList();

            _domainEvents.Clear();

            return copy;
        }
    }
}