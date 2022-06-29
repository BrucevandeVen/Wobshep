using System.Collections.Generic;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Interfaces.Interfaces
{
    public interface IShoppingCartDAL
    {
        List<ShoppingCartDTO> GetAll();
        ShoppingCartDTO GetByCustomerID(int customerID);
        int GetLastShoppingCartID();
        void Create();
        void Delete(ShoppingCartDTO shoppingCartDTO);
        void DeleteAllProducts(ShoppingCartDTO shoppingCartDTO);
        void DeleteProduct(int shoppingCartID, int productID);
        void AddProduct(int shoppingCartID, int productID);
    }
}