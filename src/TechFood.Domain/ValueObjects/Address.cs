using TechFood.Domain.Shared;

namespace TechFood.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(
            string streetName,
            string? streetNumber,
            string zipCode,
            string city,
            string state)
        {
            StreetName = streetName;
            StreetNumber = streetNumber;
            ZipCode = zipCode;
            City = city;
            State = state;
        }

        public string StreetName { get; set; }

        public string? StreetNumber { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Formatted => string.Concat(StreetName, ", ", StreetNumber);
    }
}