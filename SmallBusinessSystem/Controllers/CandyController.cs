using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmallBusinessSystem.Controllers
{
    public class CandyController : Controller
    {
        public CandyController(Candy dbContext)
        {
            _dbContext = dbContext;

        }
        public IActionResult Index() //right click index to create folder 
        {
            var listOfCandy = _dbContext.Candy.ToList();

            return View(listOfCandy);
        }
        [HttpPost]
        public IActionResult Create(Candy candyObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Candy.Add(candyObj);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Candy");
            }

            return View(candyObj);

        }
    }
}
