using TechFood.Domain.ValueObjects;

namespace TechFood.Application.Users.Data
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Address? Address { get; set; }
    }
}
