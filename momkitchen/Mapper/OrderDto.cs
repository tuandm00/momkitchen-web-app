using momkitchen.Models;

namespace momkitchen.Mapper
{
    public class OrderDto
    {
        public DateTime? Date { get; set; }
        public int? CustomerId { get; set; }

        public int? BatchId { get; set; }

        public int? BuildingId { get; set; }

        public int? Quantity { get; set; }

        public int? SessionId { get; set; }

        public string? Email { get; set; }

        public string CustomerPhone { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string? Note { get; set; }

    }
}
