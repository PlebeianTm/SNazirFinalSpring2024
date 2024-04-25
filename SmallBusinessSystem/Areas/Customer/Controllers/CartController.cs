using SmallBusinessSystem.Data;
using SmallBusinessSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallBusinessSystem.Data;
using System.Security.Claims;

namespace SmallBusinessSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private CandyDbContext _dbContext;

        public CartController(CandyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public IActionResult Index()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var cartItemList = _dbContext.Carts.Where(c => c.UserId == userId).Include(c => c.Candy);
            

        //}
    }

}
