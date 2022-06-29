using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wobshep.Logic;
using WobshepUI.Models;
using WobshepUI.Models.Product;
using WobshepUI.Models.ShoppingCart;

namespace WobshepUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        [AllowAnonymous]
        public IActionResult GetAllProducts()
        {
            ProductCollection productCollection = new ProductCollection();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (Product product in productCollection.GetProducts())
            {
                productViewModels.Add(new ProductViewModel
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ImageURL = product.ImageURL
                });
            }

            return View(productViewModels);
        }

        [AllowAnonymous]
        public IActionResult GetProduct(int id)
        {
            ProductCollection productCollection = new ProductCollection();
            ProductViewModel productViewModel = new ProductViewModel();
            Product product = new Product();
            product = productCollection.GetProduct(id);

            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price;
            productViewModel.Description = product.Description;
            productViewModel.ImageURL = product.ImageURL;

            return View(productViewModel);
        }

        public IActionResult ListProducts()
        {
            ProductCollection productCollection = new ProductCollection();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (Product product in productCollection.GetProducts())
            {
                productViewModels.Add(new ProductViewModel
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ImageURL = product.ImageURL
                });
            }

            return View(productViewModels);
        }

        public IActionResult CreateProduct(CreateProductViewModel productViewModel)
        {
            ProductCollection productCollection = new ProductCollection();

            if (!ModelState.IsValid) return View();

            Product product = new Product();
            product.Name = productViewModel.Name;
            product.Price = productViewModel.Price;
            product.Description = productViewModel.Description;
            product.ImageURL = productViewModel.ImageURL;

            productCollection.Create(product);

            return RedirectToAction("ListProducts", "Product");
        }

        public IActionResult DeleteProduct(int id)
        {
            ProductCollection productCollection = new ProductCollection();
            ProductViewModel productViewModel = new ProductViewModel();
            Product product = new Product();
            product = productCollection.GetProduct(id);

            productViewModel.ID = product.ID;
            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price;
            productViewModel.Description = product.Description;
            productViewModel.ImageURL = product.ImageURL;

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult DeleteProduct(ProductViewModel productViewModel)
        {
            ProductCollection productCollection = new ProductCollection();

            if (!ModelState.IsValid) return View();

            Product product = new Product();
            product.ID = productViewModel.ID;

            productCollection.Delete(product);

            return RedirectToAction("ListProducts", "Product");
        }

        public IActionResult UpdateProduct(int id)
        {
            ProductCollection productCollection = new ProductCollection();
            UpdateProductViewModel updateProductViewModel = new UpdateProductViewModel();
            Product product = new Product();
            product = productCollection.GetProduct(id);

            updateProductViewModel.ID = product.ID;
            updateProductViewModel.Name = product.Name;
            updateProductViewModel.Price = product.Price;
            updateProductViewModel.Description = product.Description;
            updateProductViewModel.ImageURL = product.ImageURL;

            return View(updateProductViewModel);
        }

        [HttpPost]
        public IActionResult UpdateProduct(UpdateProductViewModel updateProductViewModel)
        {
            if (!ModelState.IsValid) return View();

            Product product = new Product();
            product.ID = updateProductViewModel.ID;
            product.Name = updateProductViewModel.Name;
            product.Price = updateProductViewModel.Price;
            product.Description = updateProductViewModel.Description;
            product.ImageURL = updateProductViewModel.ImageURL;

            product.Update(product);

            return RedirectToAction("ListProducts", "Product");
        }
    }
}
