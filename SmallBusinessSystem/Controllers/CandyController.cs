using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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



        public IActionResult Details()
        {
            var listOfCandy = _dbContext.Candies.ToList();
            return View(listOfCandy);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Candy candy= _dbContext.Candies.Find(id);

            return View(candy);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("CandyId, CandyName, Description, CandyPrice,ImgUrl,CandyQty")] Candy candyObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Candies.Update(candyObj);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Candy");
            }

            return View(candyObj);
        }

    }
}
