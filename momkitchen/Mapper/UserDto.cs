using momkitchen.Models;

namespace momkitchen.Mapper
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? AccountStatus { get; set; }

        public Role? Role { get; set; }
    }
}
