using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Northwind.Business.Abstract;
using Core.Northwind.MvcWebUI.Models;
using Core.Northwind.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Northwind.MvcWebUI.Controllers
{
    [Authorize(Roles ="Admin")] //bunu yazdığımızda admin controllerında işlem yapabilmesi için sisteme giriş yapmak gerekir
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        // GET: /<controller>/
        public ActionResult Index()
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetAll()

            };
            return View(productListViewModel);
        }

        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(),
                Categories=_categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
            _productService.Add(product);
            TempData.Add("message","Product was succesfully added");
            }
            return RedirectToAction("Add");
        }

        public ActionResult Update(int ProductId)
        {
            var model= new ProductUpdateViewModel
                {
                Product = _productService.GetById(ProductId),
                Categories= _categoryService.GetAll()
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Product product)
        {

            if (ModelState.IsValid)
            {
                _productService.Update(product);
                TempData.Add("message", "Product was succesfully updated");
            }
            return RedirectToAction("Update", new { ProductId = product.ProductId });
        }

        public ActionResult Delete(int productId)
        {
            _productService.Delete(productId);
           
            TempData.Add("message","Product was succesfully deleted");
            return RedirectToAction("Index");
        }
    }
}
