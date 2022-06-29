using System;
using System.Collections.Generic;
using System.Linq;
using Wobshep.Factory;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Logic
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();
        public bool IsAdmin { get; set; }

        public void Update()
        {
            CustomerFactory.GetCustomerDAL().Update(new CustomerDTO
            {
                ID = ID,
                Name = Name,
                Password = Password,
                Email = Email,
                IsAdmin = IsAdmin
            });
        }

        public void CreateOrder()
        {
            OrderDTO orderDTO = new OrderDTO();
            orderDTO.TotalPrice = ShoppingCart.TotalPrice;
            orderDTO.CustomerDTO = new CustomerDTO();
            orderDTO.CustomerDTO.ID = ID;
            ShoppingCart.GetProducts(ShoppingCartFactory.GetShoppingCartDAL().GetByCustomerID(ID).ProductDTOs);

            foreach (Product product in ShoppingCart.Products)
            {
                orderDTO.ProductDTOs.Add(new ProductDTO
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                });
            }

            OrderFactory.GetOrderDAL().Create(orderDTO);
        }

        public void DeleteOrder(Order order)
        {
            OrderFactory.GetOrderDAL().Delete(new OrderDTO 
            {
                ID = order.ID,
                TotalPrice = order.TotalPrice,
            });
        }

        public ShoppingCart GetShoppingCart()
        {
            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO();
            shoppingCartDTO = ShoppingCartFactory.GetShoppingCartDAL().GetByCustomerID(ID);

            ShoppingCart shoppingCart = new ShoppingCart(shoppingCartDTO);

            return shoppingCart;
        }

        public void DeleteAllProducts()
        {
            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO();

            shoppingCartDTO.ID = ShoppingCart.ID;

            ShoppingCartFactory.GetShoppingCartDAL().DeleteAllProducts(shoppingCartDTO);
        }
    }
}