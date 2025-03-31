using TechFood.Domain.Shared;

namespace TechFood.Domain.ValueObjects
{
    public class DeliveryFee : ValueObject
    {
        public double Price { get; set; }

        public static implicit operator double(DeliveryFee deliveryFee) => deliveryFee.Price;
    }
}