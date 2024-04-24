using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallBusinessSystem.Data;

namespace SmallBusinessSystem.Controllers
{
    public class CartController : Controller
    {
        private CandyDbContext _dbContext;
        public CartController(CandyDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
    }

}
