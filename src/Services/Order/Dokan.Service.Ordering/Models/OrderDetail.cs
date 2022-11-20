namespace Dokan.Service.Ordering.Models
{
    public class OrderDetail
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
