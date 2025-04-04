using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities
{
    public class Category : Entity, IAggregateRoot
    {
        public string Name { get; set; } = null!;

        public string ImageFileName { get; set; } = null!;
    }
}