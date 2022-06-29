using System.Collections.Generic;
using Wobshep.Interfaces.DTOs;

namespace Wobshep.Interfaces.Interfaces
{
    public interface ICustomerDAL
    {
        List<CustomerDTO> GetAll();
        CustomerDTO GetById(int customerID);
        CustomerDTO GetCustomerByOrder(int orderID);
        CustomerDTO GetCustomerByEmail(string email);
        void Create(CustomerDTO customerDTO);
        void Delete(CustomerDTO customerDTO);
        void Update(CustomerDTO customerDTO);
    }
}