using Wobshep.Factory;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Logic
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public void Update(Product product)
        {
            ProductFactory.GetProductDAL().Update(new ProductDTO
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