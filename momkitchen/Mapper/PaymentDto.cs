namespace momkitchen.Mapper
{
    public class PaymentDto
    {

        public string Type { get; set; } = null!;

        public int OrderId { get; set; }

        public string Status { get; set; } = null!;

        public decimal Amount { get; set; }
    }
}
