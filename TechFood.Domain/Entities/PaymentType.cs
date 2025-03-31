using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities
{
    public class PaymentType : Entity, IAggregateRoot
    {
        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}