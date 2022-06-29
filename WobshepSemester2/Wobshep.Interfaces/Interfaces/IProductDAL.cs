using System.Collections.Generic;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Interfaces.Interfaces
{
    public interface IProductDAL
    {
        List<ProductDTO> GetALL();
        ProductDTO GetByID(int productID);
        void Create(ProductDTO productDTO);
        void Delete(ProductDTO productDTO);
        void Update(ProductDTO productDTO);
    }
}