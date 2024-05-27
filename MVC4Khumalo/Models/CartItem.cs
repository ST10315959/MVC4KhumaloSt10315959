namespace MVC4Khumalo.Models
{
    public class CartItem
    {
        public int Id { get; set; } // Unique ID for the CartItem
        public Product Product { get; set; } // Reference to the Product
        public int Quantity { get; set; } // Quantity of the Product

        // Calculate the total price for this CartItem
        public int TotalPrice => Product.Price * Quantity;
    }
}
