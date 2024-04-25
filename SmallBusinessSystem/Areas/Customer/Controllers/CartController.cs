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
        public IActionResult ReviewOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // fetches the user id
            var cartItemsList = _dbContext.Carts.Where(c => c.UserId == userId).Include(c => c.Candy);
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM
            {
                CartItems = cartItemsList,
                Order = new Order()

            };
            foreach (var cartItem in shoppingCartVM.CartItems)
            {
                cartItem.SubTotal = cartItem.Candy.CandyPrice * cartItem.Quantity; //subtotal for the individual cart item
                shoppingCartVM.Order.OrderTotal += cartItem.SubTotal;
            }
            shoppingCartVM.Order.ApplicationUser = _dbContext.ApplicationUsers.Find(userId);
            shoppingCartVM.Order.CustomerName = shoppingCartVM.Order.ApplicationUser.Name;
            shoppingCartVM.Order.StreetAddress = shoppingCartVM.Order.ApplicationUser.StreetAddress;
            shoppingCartVM.Order.City = shoppingCartVM.Order.ApplicationUser.City;
            shoppingCartVM.Order.State = shoppingCartVM.Order.ApplicationUser.State;
            shoppingCartVM.Order.PostalCode = shoppingCartVM.Order.ApplicationUser.PostalCode;
            shoppingCartVM.Order.Phone = shoppingCartVM.Order.ApplicationUser.PhoneNumber;
            return View(shoppingCartVM);
        }


        //[HttpPost]
        //[ActionName("ReviewOrder")]
        //public IActionResult ReviewOrderPOST(ShoppingCartVM shoppingCartVM)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // fetches the user id
        //    var cartItemsList = _dbContext.Carts.Where(c => c.UserId == userId).Include(c => c.Book);
        //    shoppingCartVM.CartItems = cartItemsList;

        //    foreach (var cartItem in shoppingCartVM.CartItems)
        //    {
        //        cartItem.SubTotal = cartItem.Candy.CandyPrice * cartItem.Quantity; //subtotal for the individual cart item
        //        shoppingCartVM.Order.OrderTotal += cartItem.SubTotal;
        //    }
        //    shoppingCartVM.Order.ApplicationUser = _dbContext.ApplicationUsers.Find(userId);
        //    shoppingCartVM.Order.CustomerName = shoppingCartVM.Order.ApplicationUser.Name;
        //    shoppingCartVM.Order.StreetAddress = shoppingCartVM.Order.ApplicationUser.StreetAddress;
        //    shoppingCartVM.Order.City = shoppingCartVM.Order.ApplicationUser.City;
        //    shoppingCartVM.Order.State = shoppingCartVM.Order.ApplicationUser.State;
        //    shoppingCartVM.Order.PostalCode = shoppingCartVM.Order.ApplicationUser.PostalCode;
        //    shoppingCartVM.Order.Phone = shoppingCartVM.Order.Phone;
        //    shoppingCartVM.Order.OrderDate = DateOnly.FromDateTime(DateTime.Now);
        //    shoppingCartVM.Order.OrderStatus = "Pending";
        //    shoppingCartVM.Order.PaymentStatus = "Pending";
        //    _dbContext.Orders.Add(shoppingCartVM.Order); // creates a new order ad generates an ORderID which can then be used to add order details
        //    _dbContext.SaveChanges();

        //    foreach (var eachCartItem in shoppingCartVM.CartItems)
        //    {
        //        OrderDetail orderDetail = new()
        //        {
        //            OrderId = shoppingCartVM.Order.OrderId,
        //            BookId = eachCartItem.BookId,
        //            Quantity = eachCartItem.Quantity,
        //            Price = eachCartItem.Book.Price
        //        };
        //        _dbContext.OrderDetails.Add(orderDetail);
        //    }
        //    _dbContext.SaveChanges();

        //    //StripeConfiguration.ApiKey = "sk_test_51P6xVMJ4nLlyIoyFPk3mDyPJIVqlTuMXnwgKDBw1J2lrGDXkOisxp8HcvIPBMWgPvEiV1K0ZnaWImyuaIFdeh9If00S6b4WsW0";
        //    var domain = Request.Scheme + "://" + Request.Host.Value + "/";
        //    var options = new Stripe.Checkout.SessionCreateOptions
        //    {
        //        SuccessUrl = "https://localhost:7032/" + $"customer/cart/orderconfirmation?id={shoppingCartVM.Order.OrderId}",
        //        //SuccessUrl = domain + $"customer/cart/orderconfirmation?id={shoppingCartVM.Order.OrderId}",
        //        CancelUrl = "https://localhost:7032/" + "customer/cart/index",
        //        //CancelUrl = domain + "customer/cart/index",
        //        LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
        //        //{
        //        //    new Stripe.Checkout.SessionLineItemOptions
        //        //    {
        //        //        Price = "price_1MotwRLkdIwHu7ixYcPLm5uZ",
        //        //        Quantity = 2,
        //        //    },
        //        // },
        //        Mode = "payment",

        //    };

        //    foreach (var eachCartItem in shoppingCartVM.CartItems)
        //    {
        //        var sessionLineItem = new SessionLineItemOptions
        //        {
        //            PriceData = new SessionLineItemPriceDataOptions()
        //            {
        //                UnitAmount = (long)(eachCartItem.Book.Price * 100), // 20.99 -> 2099
        //                Currency = "usd",
        //                ProductData = new SessionLineItemPriceDataProductDataOptions
        //                {
        //                    Name = eachCartItem.Book.BookTitle
        //                }
        //            },
        //            Quantity = eachCartItem.Quantity,
        //        };
        //        options.LineItems.Add(sessionLineItem);

        //    }
        //    var service = new Stripe.Checkout.SessionService();
        //    Session session = service.Create(options);

        //    shoppingCartVM.Order.SessionID = session.Id;
        //    _dbContext.SaveChanges();
        //    Response.Headers.Add("Location", session.Url);
        //    return new StatusCodeResult(303);

        //    //return RedirectToAction("OrderConfirmation", new { id= shoppingCartVM.Order.OrderId});
        //}


    }

}
