namespace elefanti60.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public CartItem( Product product) {
            ProductId = product.Id;
            ProductName = ProductName;
            Price = product.Price;
            Quantity = 1;
        }
    }
}
