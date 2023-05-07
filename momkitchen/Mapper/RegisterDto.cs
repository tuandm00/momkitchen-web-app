namespace momkitchen.Mapper
{
    public class RegisterDto
    {
        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? RoleId { get; set; }

        public string? AccountStatus { get; set; }
    }
}
