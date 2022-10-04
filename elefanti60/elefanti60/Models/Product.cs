using System.ComponentModel.DataAnnotations;

namespace elefanti60.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
