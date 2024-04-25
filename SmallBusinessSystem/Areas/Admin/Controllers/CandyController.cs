using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SmallBusinessSystem.Data;
using SmallBusinessSystem.Models;
using System;
using System.IO;

namespace SmallBusinessSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CandyController : Controller
    {
        private CandyDbContext _dbContext;
        private IWebHostEnvironment _environment;

        public CandyController(CandyDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
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


                string wwwrootPath = _environment.WebRootPath;

            if (imgFile != null)
            {
                using (var fileStream = new FileStream(Path.Combine(wwwrootPath, @"Images\CandyImages\" + imgFile.FileName), FileMode.Create))
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Candy candy = _dbContext.Candies.Find(id);

            return View(candy);
        }

        [HttpPost]
        public IActionResult Edit(Candy candyObj, IFormFile? imgFile)
        {
            string wwwrootPath = _environment.WebRootPath;

            if (ModelState.IsValid) 
            {
                if (imgFile != null) 
                {
                    if (!string.IsNullOrEmpty(candyObj.ImgUrl)) 
                    {
                        var oldImgPath = Path.Combine(wwwrootPath, candyObj.ImgUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(wwwrootPath, @"Images\CandyImages\" + imgFile.FileName), FileMode.Create))
                    {

                        imgFile.CopyTo(fileStream);

                    }

                    candyObj.ImgUrl = @"\Images\CandyImages\" + imgFile.FileName;

                }

                _dbContext.Update(candyObj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(candyObj); 
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Candy candy = _dbContext.Candies.Find(id);

            return View(candy);
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Candy candyObj = _dbContext.Candies.Find(id);//fetches the record


            // Delete associated image file if it exists
            if (!string.IsNullOrEmpty(candyObj.ImgUrl))
            {
                string wwwrootPath = _environment.WebRootPath;
                var imgPath = Path.Combine(wwwrootPath, candyObj.ImgUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }

            _dbContext.Candies.Remove(candyObj);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
