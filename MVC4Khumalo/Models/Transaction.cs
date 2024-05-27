using System.ComponentModel.DataAnnotations;

namespace MVC4Khumalo.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [RegularExpression(@"^\d{11,16}$", ErrorMessage = "Please enter a valid card number between 11 and 16 digits.")]
        public string CardNumber { get; set; }

        
        [RegularExpression(@"^\d{1,3}$", ErrorMessage = "CVV must be a 3-digit number.")]
        public string CVV { get; set; }
    }
}
