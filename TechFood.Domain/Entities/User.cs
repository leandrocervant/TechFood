using TechFood.Domain.Shared.Entities;
using TechFood.Domain.ValueObjects;

namespace TechFood.Domain.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Address Address { get; set; } = null!;
    }
}
