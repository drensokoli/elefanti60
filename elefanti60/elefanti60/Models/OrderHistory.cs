namespace elefanti60.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public static List<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
