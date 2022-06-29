using System.Collections.Generic;
using Wobshep.Factory;
using Wobshep.Interfaces.DTOs;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.Logic
{
    public class ProductCollection
    {
        private IProductDAL productDataAccess = ProductFactory.GetProductDAL();

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            foreach (ProductDTO productDTO in productDataAccess.GetALL())
            {
                products.Add(new Product
                {
                    ID = productDTO.ID,
                    Name = productDTO.Name,
                    Price = productDTO.Price,
                    Description = productDTO.Description,
                    ImageURL = productDTO.ImageURL
                });
            }

            return products;
        }

        public Product GetProduct(int id)
        {
            var productDTO = productDataAccess.GetByID(id);

            return new Product
            {
                ID = productDTO.ID,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Description = productDTO.Description,
                ImageURL = productDTO.ImageURL
            };
        }

        public void Create(Product product)
        {
            productDataAccess.Create(ProductToDTO(product)); 
        }

        public void Delete(Product product)
        {
            productDataAccess.Delete(ProductToDTO(product));
        }

        private ProductDTO ProductToDTO(Product product)
        {
            return new ProductDTO
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageURL = product.ImageURL
            };
        }
    }
}
