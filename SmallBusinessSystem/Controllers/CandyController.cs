using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallBusinessSystem.Data;
using SmallBusinessSystem.Models;

namespace SmallBusinessSystem.Controllers
{
    public class CandyController : Controller
    {
        private CandyDbContext _dbContext;
        public CandyController(CandyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index() //right click index to create folder 
        {
            var listOfCandy = _dbContext.Candies.ToList();

            return View(listOfCandy);
        }
        [HttpPost]
        public IActionResult Create(Candy candyObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Candies.Add(candyObj);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Candy");
            }

            return View(candyObj);

        }

        public IActionResult Details(int id)
        {
            var listOfCandy = _dbContext.Candies.ToList();
            return View(listOfCandy);
        }
    }
}
