using SmallBusinessSystem.Data;
using SmallBusinessSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace SmallBusinessSystem.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private CandyDbContext _dbContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CandyDbContext candyDBConext)
        {
            _logger = logger;
            _dbContext = candyDBConext;
        }

        public IActionResult Index()
        {
            var listOfCandies = _dbContext.Candies;
            return View(listOfCandies.ToList());
        }

        public IActionResult Details(int id)
        {
            Candy candy = _dbContext.Candies.Find(id);


            var cart = new Cart
            {
                CandyId = id,
                Candy = candy,
                Quantity = 1
            };

            //create cart obj and pass to the view
            return View(cart);

        }
        //[HttpPost]
        //[Authorize]
        //public IActionResult Details(Cart cart)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //fetches user ID

        //    cart.UserId = userId;

        //    Cart existingCart = _dbContext.Carts.FirstOrDefault(c => c.UserId == userId && c.BookId == cart.BookId);

        //    if (existingCart != null)
        //    {
        //        //update cart

        //        existingCart.Quantity += cart.Quantity;
        //    }
        //    else
        //    {
        //        _dbContext.Carts.Add(cart);
        //    }


        //    _dbContext.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
