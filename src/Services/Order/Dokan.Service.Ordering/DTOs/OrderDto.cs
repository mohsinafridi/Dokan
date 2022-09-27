namespace Dokan.Service.Ordering.DTOs
{
    public class OrderDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
