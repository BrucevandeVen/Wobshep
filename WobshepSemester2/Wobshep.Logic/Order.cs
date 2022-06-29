using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Wobshep.Factory;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Logic
{
    public class Order
    {
        public int ID { get; set; }
        public decimal TotalPrice { get; set; }
        public Customer Customer { get; set; } = new Customer();
        public IEnumerable<Product> Products { get; set; }

        public Order(OrderDTO orderDTO)
        {
            ID = orderDTO.ID;
            Customer = GetCustomer();
            Products = GetProducts(orderDTO.ProductDTOs);
            TotalPrice = orderDTO.TotalPrice;
        }

        public Order()
        { 

        }

        private Customer GetCustomer()
        {
            var customerDTO = CustomerFactory.GetCustomerDAL().GetCustomerByOrder(ID);

            Customer customer = new Customer();
            customer.ID = customerDTO.ID;
            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.Password = customerDTO.Password;
            customer.ShoppingCart.ID = customerDTO.ShoppingCartDTO.ID;
            customer.IsAdmin = customerDTO.IsAdmin;

            return customer;
        }

        public IEnumerable<Product> GetProducts(List<ProductDTO> productDTOs)
        {
            List<Product> products = new List<Product>();

            foreach (ProductDTO productDTO in productDTOs)
            {
                products.Add(new Product
                {
                    ID = productDTO.ID,
                    Name = productDTO.Name,
                    Price = productDTO.Price,
                    Description = productDTO.Description
                });
            }

            return products;
        }

        public void Update(Order order)
        {
            OrderFactory.GetOrderDAL().Update(new OrderDTO() 
            {
                ID = order.ID,
                TotalPrice = order.TotalPrice,
            });
        }

        public void AddProduct(Product product)
        {
            OrderFactory.GetOrderDAL().AddProduct(new OrderDTO
            {
                ID = ID,
                TotalPrice = TotalPrice
            },
            new ProductDTO
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            });
        }

        public void DeleteProduct(Product product)
        {
            OrderFactory.GetOrderDAL().DeleteProduct(new OrderDTO
            {
                ID = ID,
                TotalPrice = TotalPrice
            },
            new ProductDTO
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageURL = product.ImageURL
            });
        }
    }
}
