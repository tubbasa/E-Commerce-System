using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Northwind.MvcWebUI.Services;
using Core.Northwind.Business.Abstract;
using Core.Northwind.MvcWebUI.Models;
using Core.Northwind.Entities.Concrete;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Northwind.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        private IProductService _productService;

        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _cartSessionService = cartSessionService;
            _productService = productService;
        }
        // GET: /<controller>/
        public ActionResult AddToCart(int productId)
        {
            var productToBeAdded = _productService.GetById(productId);

            var cart = _cartSessionService.GetCart();

            _cartService.AddToCart(cart, productToBeAdded);
            _cartSessionService.SetCart(cart);

            TempData.Add("message",String.Format("Your Product,{0} was succesfully added to the cart!",productToBeAdded.ProductName));
           return RedirectToAction("Index","Product");
        }

        public ActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            CartSummaryViewModel cartLİstViewModel = new CartSummaryViewModel
            {
                Cart = cart
            };
            return View(cartLİstViewModel);
        }

        public ActionResult Remove(int productId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart,productId);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", String.Format("Your Product was succesfully added to the cart!"));
            return RedirectToAction("List");

        }

        public ActionResult Complete()
        {
            var shippingDetailViewModel = new ShippingDetailsViewModel
            {
                ShippingDetails = new ShippingDetails()

            };
            return View(shippingDetailViewModel);
        }

        [HttpPost]
        public ActionResult Complete(ShippingDetails shippingDetails)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                TempData.Add("message", String.Format("Thank you {0}, your order is in process",shippingDetails.FirstName));
                return View();
            }
        }
    }
}
