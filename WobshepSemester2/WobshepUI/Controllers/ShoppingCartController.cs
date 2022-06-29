using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WobshepUI.Models.ShoppingCart;
using Wobshep.Logic;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace WobshepUI.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        public IActionResult ListShoppingCartProducts()
        {
            Customer customer = new Customer
            {
                ID = Convert.ToInt32(User.Claims.First(claim => claim.Type == "ID").Value)
            };

            List<ShoppingCartViewModel> shoppingCartViewModels = new List<ShoppingCartViewModel>();
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart = customer.GetShoppingCart();

            foreach (Product product in shoppingCart.Products)
            {
                ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price
                };

                shoppingCartViewModel.TotalPrice = shoppingCart.TotalPrice;

                shoppingCartViewModels.Add(shoppingCartViewModel);
            }

            return View(shoppingCartViewModels);
        }

        public IActionResult AddToShoppingCart(int id)
        {
            int customerID = Convert.ToInt32(User.Claims.First(claim => claim.Type == "ID").Value);

            CustomerCollection customerCollection = new CustomerCollection();
            Customer customer = new Customer();
            customer = customerCollection.GetCustomer(customerID);
            
            ShoppingCart checkCart = customer.GetShoppingCart();
            if(checkCart.Products.Any(product => product.ID == id))
            {
                return RedirectToAction("GetAllProducts", "Product");
            }

            ShoppingCart shoppingCart = new ShoppingCart
            {
                ID = customer.ShoppingCart.ID
            };

            shoppingCart.AddProduct(id);

            return RedirectToAction("GetAllProducts", "Product");
        }

        public IActionResult DeleteFromShoppingCart(int id)
        {
            int customerID = Convert.ToInt32(User.Claims.First(claim => claim.Type == "ID").Value);

            CustomerCollection customerCollection = new CustomerCollection();
            Customer customer = new Customer();
            customer = customerCollection.GetCustomer(customerID);

            ShoppingCart shoppingCart = new ShoppingCart
            {
                ID = customer.ShoppingCart.ID
            };

            shoppingCart.DeleteProduct(id);

            return RedirectToAction("ListShoppingCartProducts", "ShoppingCart");
        }

        public IActionResult CreateOrder()
        {
            CustomerCollection customerCollection = new CustomerCollection();
            Customer customer = new Customer();
            customer = customerCollection.GetCustomer(Convert.ToInt32(User.Claims.First(claim => claim.Type == "ID").Value));

            customer.GetShoppingCart();

            customer.CreateOrder();

            customer.DeleteAllProducts();

            return RedirectToAction("ListShoppingCartProducts", "ShoppingCart");
        }

        public IActionResult ListOrders()
        {
            return View();
        }
    }
}
