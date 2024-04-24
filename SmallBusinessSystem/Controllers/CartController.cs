using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallBusinessSystem.Data;
using System.Security.Claims;

namespace SmallBusinessSystem.Controllers
{
    public class CartController : Controller
    {
        private CandyDbContext _dbContext;
        public CartController(CandyDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var cartItemList = _dbContext.Candies.Where();
        }
    }

}
