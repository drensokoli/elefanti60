namespace elefanti60.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public decimal Total { get; set; }
    }
}
