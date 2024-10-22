using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GameZone.Controllers
{
    [Authorize]
    public class CartItemController : Controller
    {
       
        private readonly ICartService _cartService;
        //private readonly ApplicationDbContext _context;

        public CartItemController( ICartService cartService)
        {

            _cartService = cartService;
           
        }
        // Display the cart items
        public IActionResult Index()
        {
            var cartItems = _cartService.GetAll();


            return View(cartItems);
        }

      
        public ActionResult Cart()
        {
            var cart = _cartService.DisplayCart();
            return View("AddToCart", cart);
        }


        public ActionResult AddToCart(int id )
        {

            var items = _cartService.AddToCart(id);
            return View(items);
           
        }


        


        public IActionResult DeleteFromCart(int id)
        {
           
            var isDeleted = _cartService.Delete(id);


            return isDeleted ? RedirectToAction("Index") : BadRequest();

        }


        public IActionResult SubmitAndDelete()
        {
            
                // Delete all items from the cart
                var cartItems = _cartService.GetAll();
                foreach (var cartItem in cartItems)
                {
                    _cartService.Delete(cartItem.Id);
                }

                // Redirect to the Submit view
                return View("Submit");
        }
        //public IActionResult Submit()
        //{
        //    return View();
        //}
    }
}
