using SmallBusinessSystem.Data;
using SmallBusinessSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallBusinessSystem.Data;
using System.Security.Claims;
using SmallBusinessSystem.Models.ViewModels;

namespace SmallBusinessSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    //[Authorize(Roles = "Customer")]
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
            var cartItemList = _dbContext.Carts.Where(c => c.UserId == userId).Include(c => c.Candy);
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM
            {
                CartItems = cartItemList,
                Order = new Order()

            };
            foreach(var cartItem in shoppingCartVM.CartItems) 
            {
                cartItem.SubTotal = cartItem.Candy.CandyPrice * cartItem.Quantity;
                shoppingCartVM.Order.OrderTotal += cartItem.SubTotal;
            }
            return View(shoppingCartVM);
        }
        public IActionResult IncrementByOne(int id)
        {
            Cart cart = _dbContext.Carts.Find(id);
            cart.Quantity++;
            _dbContext.Update(cart);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DecrementByOne(int id)
        {
            Cart cart = _dbContext.Carts.Find(id);
            if (cart.Quantity <= 1)
            {
                //remove the item
                _dbContext.Carts.Remove(cart);
                _dbContext.SaveChanges();
            }
            else
            {
                cart.Quantity--;
                _dbContext.Update(cart);
                _dbContext.SaveChanges();
            }


            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int id)
        {
            Cart cart = _dbContext.Carts.Find(id);
            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
