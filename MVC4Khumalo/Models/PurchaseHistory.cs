namespace MVC4Khumalo.Models
{
    public class PurchaseHistory
    {
        public int Id { get; set; } // Unique ID for the PurchaseHistory
        public string FirstName { get; set; } // Taken from Transaction model
        public string LastName { get; set; }  // Taken from Transaction model
        public List<CartItem> PurchasedItems { get; set; } = new List<CartItem>(); // List of purchased items
        public DateTime PurchaseDate { get; set; } // Date and time of the purchase

        // Calculate the total price of all purchased items
        public int TotalPrice => PurchasedItems.Sum(item => item.TotalPrice);
    }
}
