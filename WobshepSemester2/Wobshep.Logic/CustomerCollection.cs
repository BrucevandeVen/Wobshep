using System.Collections.Generic;
using Wobshep.Factory;
using Wobshep.Interfaces.DTOs;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.Logic
{
    public class CustomerCollection
    {
        private ICustomerDAL CustomerDataAccess = CustomerFactory.GetCustomerDAL();
        private IShoppingCartDAL ShoppingCartDataAccess = ShoppingCartFactory.GetShoppingCartDAL();

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            foreach (CustomerDTO customerDTO in CustomerDataAccess.GetAll())
            {
                customers.Add(DTOToCustomer(customerDTO));
            }

            return customers;
        }

        public Customer GetCustomer(int id)
        {
            return DTOToCustomer(CustomerDataAccess.GetById(id));
        }

        public Customer GetCustomerByEmail(string email)
        {
            return DTOToCustomer(CustomerDataAccess.GetCustomerByEmail(email));
        }

        public void CreateCustomer(Customer customer)
        {
            ShoppingCartDataAccess.Create();

            CustomerDTO customerDTO = CustomerToDTO(customer);

            customerDTO.ShoppingCartDTO.ID = ShoppingCartDataAccess.GetLastShoppingCartID();

            CustomerDataAccess.Create(customerDTO);
        }

        public void DeleteCustomer(Customer customer)
        {
            CustomerDataAccess.Delete(CustomerToDTO(customer));
            ShoppingCartDataAccess.Delete(CustomerToDTO(customer).ShoppingCartDTO);
        }

        private CustomerDTO CustomerToDTO(Customer customer)
        {
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.ID = customer.ID;
            customerDTO.Name = customer.Name;
            customerDTO.Email = customer.Email;
            customerDTO.Password = customer.Password;
            customerDTO.IsAdmin = customer.IsAdmin;
            customerDTO.ShoppingCartDTO.ID = customer.ShoppingCart.ID;

            return customerDTO;
        }

        private Customer DTOToCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.ID = customerDTO.ID;
            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.Password = customerDTO.Password;
            customer.ShoppingCart.ID = customerDTO.ShoppingCartDTO.ID;
            customer.IsAdmin = customerDTO.IsAdmin;

            return customer;
        }
    }
}