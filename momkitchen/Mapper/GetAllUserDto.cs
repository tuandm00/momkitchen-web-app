namespace momkitchen.Mapper
{
    public class GetAllUserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string Phone { get; set; } = null!;

        public string? Image { get; set; }

        public string Email { get; set; } = null!;

        public int? DefaultBuilding { get; set; }

        public string? Address { get; set; }

        public int? BuildingId { get; set; }

        public int? BatchId { get; set; }

    }
}