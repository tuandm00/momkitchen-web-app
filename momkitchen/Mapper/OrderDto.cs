using momkitchen.Models;

namespace momkitchen.Mapper
{
    public class OrderDto
    {
        public int? BuildingId { get; set; }
        public int? SessionId { get; set; }
        public string? Email { get; set; }
        public string? CustomerPhone { get; set; }
        public int? Quantity { get; set; }
        public string? Note { get; set; }
        public int? TotalPrice { get; set; }
        public virtual ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
        public virtual ICollection<PaymentDto1> Payments { get; set; } = new List<PaymentDto1>();
        public virtual ICollection<SessionPackageDto1> SessionPackages { get; set; } = new List<SessionPackageDto1>();



    }

    public class OrderDetailDto
    {
        public int SessionPackageId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }


    }

    public class PaymentDto1
    {
        public decimal Amount { get; set; }
    }

    public class SessionPackageDto1
    {
        public int? RemainQuantity { get; set; }

    }

}



