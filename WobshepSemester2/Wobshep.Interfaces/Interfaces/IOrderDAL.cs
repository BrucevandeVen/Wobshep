using System;
using System.Collections.Generic;
using System.Text;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Interfaces.Interfaces
{
    public interface IOrderDAL
    {
        List<OrderDTO> GetAll();
        OrderDTO GetByCustomer(int customerID);
        void Create(OrderDTO orderDTO);
        void Delete(OrderDTO orderDTO);
        void Update(OrderDTO orderDTO);
        void AddProduct(OrderDTO orderDTO, ProductDTO productDTO);
        void DeleteProduct(OrderDTO orderDTO, ProductDTO productDTO);
    }
}
