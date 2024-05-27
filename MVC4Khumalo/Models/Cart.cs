namespace MVC4Khumalo.Models
{
    public class Cart
    {
        public int Id { get; set; } // Unique ID for the Cart
        public List<CartItem> Items { get; set; } = new List<CartItem>(); // List of CartItems

        // Calculate the total price of all items in the cart
        public int TotalPrice => Items.Sum(item => item.TotalPrice);
    }
}
