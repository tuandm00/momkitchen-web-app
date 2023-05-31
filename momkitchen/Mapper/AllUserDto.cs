namespace momkitchen.Mapper
{
    public class AllUserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string Phone { get; set; } = null!;

        public string? Image { get; set; }

        public string Email { get; set; } = null!;

        public int? DefaultBuilding { get; set; }
    }

    public class ChefDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Image { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public int? BuildingId { get; set; }
    }

    public class ShipperDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public int? BatchId { get; set; }

        public string? Image { get; set; }
    }
}
