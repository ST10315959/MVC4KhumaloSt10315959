using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC4Khumalo.Data;


namespace MVC4Khumalo.Controllers
{
    [Authorize]
    public class PurchaseHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PurchaseHistoryController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var purchaseHistories = _context.PurchaseHistories
                .Include(ph => ph.PurchasedItems)
                .ThenInclude(pi => pi.Product)
                .ToList();
            return View(purchaseHistories);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseHistory = await _context.PurchaseHistories.FindAsync(id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            _context.PurchaseHistories.Remove(purchaseHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
