using BusinessLogic.Contracts;
using DataAccessLayer.Entities;
using DietTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using DietTracker.Views.Shared.Components.SearchBar;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace DietTracker.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "admin, user")]
        //[HttpGet]
        public IActionResult Index(string SearchText = "", int pg = 1)
        {
            //SPager SearchPager = new SPager() { Action = "Index", Controller = "Product", SearchText = SearchText };
            //ViewBag.SearchPager = SearchPager;
            if (SearchText != "" && SearchText != null)
            {
                var viewModel = new ProductsViewModel
                {
                    Products = _productService.GetAll()
                     .Where(a => a.ProductName.Contains(SearchText)).ToList()
                };
                //return View(viewModel);
                const int pageSize = 10;
                if (pg < 1)
                    pg = 1;
                int recsCount = viewModel.Products.Count();

                int recSkip = (pg - 1) * pageSize;
                viewModel.Products = viewModel.Products.Skip(recSkip).Take(pageSize).ToList();
                SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "Index", Controller = "Product", SearchText = SearchText };
                ViewBag.SearchPager = SearchPager;
                return View(viewModel);
                
            }
            else
            {

            var viewModel = new ProductsViewModel
            {
                Products = _productService.GetAll().ToList()
            };
                const int pageSize = 10;
                if (pg < 1)
                    pg = 1;
                int recsCount = viewModel.Products.Count();

                int recSkip = (pg - 1) * pageSize;
                viewModel.Products = viewModel.Products.Skip(recSkip).Take(pageSize).ToList();
                SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "Index", Controller = "Product", SearchText = SearchText };
                ViewBag.SearchPager = SearchPager;
                return View(viewModel);
                //return View(viewModel);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel productModel)
        {
            var product = new Product
            {
                ProductName = productModel.NewProductName,
                Fat = productModel.Fat,
                Protein = productModel.Protein,
                Carbs = productModel.Carbs,
                Calories = productModel.Calories,
            };
            await _productService.CreateAsync(product);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            var model = new AddProductViewModel(){};
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var model = new EditProductViewModel
            {
                Product = _productService.GetAll().FirstOrDefault(d => d.Id == id)
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            var product = _productService.GetAll().FirstOrDefault(d => d.Id == model.Product.Id);
            //rewrite all this bullshit
            product.ProductName = model.Product.ProductName;
            product.Protein = model.Product.Protein;
            product.Carbs = model.Product.Carbs;
            product.Calories = model.Product.Calories;
            product.Fat = model.Product.Fat;
            await _productService.UpdateAsync(product);
            return RedirectToAction("Index", new { id = model.Product.Id });
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productToDelete = _productService.GetAll().FirstOrDefault(d => d.Id == id);
            await _productService.DeleteAsync(productToDelete);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Check in model does product with such product name exist
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckProductName(string newProductName)
        {
            var product = _productService.GetAll().FirstOrDefault(d => d.ProductName == newProductName);
            if (product is not null)
                return Json(false);
            return Json(true);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
