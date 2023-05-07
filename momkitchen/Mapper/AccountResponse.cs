using momkitchen.Models;

namespace momkitchen.Mapper
{
    public class AccountResponse
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }

        public string? AccountStatus { get; set; }

        public virtual Role? Role { get; set; }
    }
}
