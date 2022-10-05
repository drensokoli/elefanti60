﻿namespace elefanti60.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public CartItem( Product product) {
            ProductId = product.Id;
            ProductName = product.Title;
            Price = product.Price;
            Quantity = 1;
        }
    }
}
