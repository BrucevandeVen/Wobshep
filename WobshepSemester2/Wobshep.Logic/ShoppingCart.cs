using System.Collections.Generic;
using Wobshep.Factory;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Logic
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<Product> Products { get; }

        public ShoppingCart(ShoppingCartDTO shoppingCartDTO)
        {
            ID = shoppingCartDTO.ID;
            Products = GetProducts(shoppingCartDTO.ProductDTOs);
            TotalPrice = SumPrice(Products);
        }

        public ShoppingCart()
        {
            Products = new List<Product>();
        }

        private decimal SumPrice(IEnumerable<Product> products)
        {
            decimal totalPrice = 0;

            foreach (Product product in products)
            {
                totalPrice += product.Price;
            }

            return totalPrice;
        }

        public IEnumerable<Product> GetProducts(List<ProductDTO> productDTOs)
        {
            List<Product> products = new List<Product>();

            if (productDTOs != null)
            {
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
            }

            return products;
        }

        public void AddProduct(int productID)
        {
            ShoppingCartFactory.GetShoppingCartDAL().AddProduct(ID, productID);
        }

        public void DeleteProduct(int productID)
        {
            ShoppingCartFactory.GetShoppingCartDAL().DeleteProduct(ID, productID);
        }
    }
}