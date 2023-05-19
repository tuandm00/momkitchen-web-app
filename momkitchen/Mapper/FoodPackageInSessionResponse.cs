namespace momkitchen.Mapper
{
    public class FoodPackageInSessionResponse
    {
        public int Id { get; set; }

        public int? FoodPackageId { get; set; }

        public int? SessionId { get; set; }

        public int? OrderId { get; set; }

        public int? Price { get; set; }

        public int? Quantity { get; set; }

        public int? RemainQuantity { get; set; }

        public int? Status { get; set; }

        public string? CreateDate { get; set; }
    }
}
