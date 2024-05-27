using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC4Khumalo.Data;
using MVC4Khumalo.Models;
using Newtonsoft.Json;


namespace MVC4Khumalo.Controllers
{
    [Authorize]


    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            decimal totalPrice = CalculateTotalPrice(cart);
            ViewBag.TotalPrice = totalPrice; // Pass total price to the view
            return View(cart);
        }
        private decimal CalculateTotalPrice(Cart cart)
        {
            decimal totalPrice = 0;
            foreach (var item in cart.Items)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }
            return totalPrice;
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _context.Product.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Check if the requested quantity is greater than the available quantity
            if (quantity > product.Quantity)
            {
                TempData["ErrorMessage"] = "Cannot add more quantity than available in stock.";
                return RedirectToAction("Index", "Home"); // Redirect to home or display products page
            }

            var cart = GetCart();
            var cartItem = cart.Items.FirstOrDefault(i => i.Product.Id == productId);
            if (cartItem != null)
            {
                // Check if adding the requested quantity exceeds the available quantity
                if (cartItem.Quantity + quantity > product.Quantity)
                {
                    TempData["ErrorMessage"] = "Cannot add more quantity than available in stock.";
                    return RedirectToAction("Index", "Home"); // Redirect to home or display products page
                }

                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem { Product = product, Quantity = quantity });
            }

            // Deduct quantity from product stock
            product.Quantity -= quantity;

            _context.SaveChanges();

            SaveCart(cart);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var cartItem = cart.Items.FirstOrDefault(i => i.Product.Id == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.Items.FirstOrDefault(i => i.Product.Id == productId);
            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (cart.Items.Any())
            {
                // Redirect to the Transactions/Create view for transaction details
                return RedirectToAction("Create", "Transactions");
            }
            return RedirectToAction("Index");
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetString("Carts");
            return cart == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(cart);
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetString("Carts", JsonConvert.SerializeObject(cart));
        }
    }

}
