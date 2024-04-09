using Microsoft.AspNetCore.Mvc;
using Microsoft.Data; 

using Microsoft.EntityFrameworkCore;
using SmallBusinessSystem.Data;
using SmallBusinessSystem.Models;
using System.IO;

namespace SmallBusinessSystem.Controllers
{
    public class CandyController : Controller
    {
        private CandyDbContext _dbContext;
        private IWebHostEnvironment _enviornment;
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
        public IActionResult Create(Candy candyObj, IFormFile imgFile)
        {
            if (ModelState.IsValid) 
            {
                string wwwrootPath = _enviornment.WebRootPath;

                if(imgFile != null) 
                {
                    using (var fileStream = new FileStream(Path.Combine(wwwrootPath, @"Images/CandyImages/" + imgFile.FileName), FileMode.Create))
                    {
                        imgFile.CopyTo(fileStream);
                    }
                    candyObj.ImgUrl = @"\Images\CandyImages\" + imgFile.FileName;
                }
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
    }
}
