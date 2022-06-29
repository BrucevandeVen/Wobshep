using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wobshep.Logic;
using WobshepUI.Models;
using WobshepUI.Models.Customer;

namespace WobshepUI.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            // Lets first check if the Model is valid or not
            if (!ModelState.IsValid) return View(loginViewModel);

            // Then try to find the user; if not exists we will not try to login
            CustomerCollection customerCollection = new CustomerCollection();

            Customer customer = customerCollection.GetCustomerByEmail(loginViewModel.Email);
            if (customer.Email == null)
            {
                ModelState.AddModelError("", "The email provided is incorrect");
                return View(loginViewModel);
            }

            // INFO: be smart: use whichever standard implementation the framework provides. Most of the time it is better then what we can come up with)
            PasswordHasher<Customer> hasher = new PasswordHasher<Customer>();

            if (hasher.VerifyHashedPassword(customer, customer.Password, loginViewModel.Password) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "The password provided is incorrect.");
                return View(loginViewModel);
            }

            //INFO: Claims are use to specify properties of an Identity(a user)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.Name),
                new Claim("ID", Convert.ToString(customer.ID)),
                new Claim(ClaimTypes.Role, Convert.ToString(customer.IsAdmin))
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("GetAllProducts", "Product");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("GetAllProducts", "Product");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            // Lets first check if the Model is valid or not
            if (!ModelState.IsValid) return View(registerViewModel);

            CustomerCollection customerCollection = new CustomerCollection();

            if (customerCollection.GetCustomerByEmail(registerViewModel.Email).Email != null)
            {
                ModelState.AddModelError("", "This email already exists, please use a unique email");
                return View(registerViewModel);
            }
             
            // INFO: be smart: use whichever standard implementation the framework provides. Most of the time it is better then what we can come up with)
            PasswordHasher<Customer> hasher = new PasswordHasher<Customer>();

            string hashedPassword = hasher.HashPassword(new Customer(), registerViewModel.Password);

            Customer customer = new Customer();
            customer.Name = registerViewModel.Name;
            customer.Email = registerViewModel.Email;
            customer.Password = hashedPassword;
            customer.IsAdmin = registerViewModel.IsAdmin;

            customerCollection.CreateCustomer(customer);

            return RedirectToAction("Login", "Customer");
        }

        public IActionResult ListCustomers()
        {
            CustomerCollection customerCollection = new CustomerCollection();
            List<CustomerViewModel> customers = new List<CustomerViewModel>();

            foreach (Customer customer in customerCollection.GetCustomers())
            {
                customers.Add(new CustomerViewModel 
                { 
                    ID = customer.ID,
                    Name = customer.Name, 
                    Email = customer.Email, 
                    Password = customer.Password, 
                    ShoppingCartID = customer.ShoppingCart.ID,
                    IsAdmin = customer.IsAdmin
                });
            }

            return View(customers);
        }

        public IActionResult UpdateCustomer(int id)
        {
            CustomerCollection customerCollection = new CustomerCollection();
            UpdateCustomerViewModel updateCustomerViewModel = new UpdateCustomerViewModel();
            Customer customer = new Customer();
            customer = customerCollection.GetCustomer(id);

            updateCustomerViewModel.ID = customer.ID;
            updateCustomerViewModel.Name = customer.Name;
            updateCustomerViewModel.Email = customer.Email;
            updateCustomerViewModel.IsAdmin = customer.IsAdmin;

            return View(updateCustomerViewModel);
        }

        [HttpPost]
        public IActionResult UpdateCustomer(UpdateCustomerViewModel updateCustomerViewModel)
        {
            if (!ModelState.IsValid) return View(updateCustomerViewModel);

            PasswordHasher<Customer> hasher = new PasswordHasher<Customer>();

            string hashedPassword = hasher.HashPassword(new Customer(), updateCustomerViewModel.Password);

            Customer customer = new Customer();
            customer.ID = updateCustomerViewModel.ID;
            customer.Name = updateCustomerViewModel.Name;
            customer.Email = updateCustomerViewModel.Email;
            customer.Password = hashedPassword;
            customer.IsAdmin = updateCustomerViewModel.IsAdmin;

            customer.Update();

            return RedirectToAction("ListCustomers", customer);
        }

        public IActionResult DeleteCustomer(int id)
        {
            CustomerCollection customerCollection = new CustomerCollection();
            CustomerViewModel customerViewModel = new CustomerViewModel();
            Customer customer = new Customer();
            customer = customerCollection.GetCustomer(id);

            customerViewModel.ID = customer.ID;
            customerViewModel.Name = customer.Name;
            customerViewModel.Email = customer.Email;
            customerViewModel.Password = customer.Password;
            customerViewModel.IsAdmin = customer.IsAdmin;
            customerViewModel.ShoppingCartID = customer.ShoppingCart.ID;

            return View(customerViewModel);
        }

        [HttpPost]
        public IActionResult DeleteCustomer(CustomerViewModel customerViewModel)
        {
            CustomerCollection customerCollection = new CustomerCollection();
            Customer customer = new Customer();

            customer = customerCollection.GetCustomer(customerViewModel.ID);

            customerCollection.DeleteCustomer(customer);

            return RedirectToAction("ListCustomers", customer);
        }
    }
}
