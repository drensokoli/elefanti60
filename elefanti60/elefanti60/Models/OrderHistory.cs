namespace elefanti60.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<OrderItem> OrderedItems { get; set; }
        public decimal Total { get; set; }
    }
}
