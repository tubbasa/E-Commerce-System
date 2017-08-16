using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Northwind.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Core.Northwind.MvcWebUI.ExtensionMethods;

namespace Core.Northwind.MvcWebUI.Services
{
    public class CartSessionService : ICartSessionService
    {
        //sesison ensnesi controlelr odaklı bir nesnedr ve controllerda kullanabilirsin controllerda kullandığın şeyşer enjekte etmek için aşağıdaki kodu kullanmalısın

        private IHttpContextAccessor _httpContextAccessor;
        public CartSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Cart GetCart()
        {

         Cart cartToCheck=   _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            if (cartToCheck==null)
            {
                _httpContextAccessor.HttpContext.Session.SetObject("cart",new Cart());
                cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
               
            }
            return cartToCheck;
        }

        public void SetCart(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObject("cart", cart);
        }
    }
}
