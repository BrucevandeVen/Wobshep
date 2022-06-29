using System;
using System.Collections.Generic;
using System.Data;
using Wobshep.Interfaces;
using Wobshep.DAL;
using Wobshep.Interfaces.Interfaces;
using Wobshep.Interfaces.DTOs;
using System.Data.SqlClient;

namespace Wobshep.DAL
{
    public class CustomerDAL : ICustomerDAL
    {
        private DatabaseDAL database = new DatabaseDAL();

        public List<CustomerDTO> GetAll()
        {
            SqlCommand getCustomers = new SqlCommand($"SELECT Customer_ID, Name, Email, Password, ShoppingCart_ID, IsAdmin " +
                                                     $"FROM Customer;");

            DataTable dataTable = database.Query(getCustomers);

            List<CustomerDTO> customerDTOs = new List<CustomerDTO>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                CustomerDTO customerDTO = new CustomerDTO();
                customerDTOs.Add(CustomerDTOFill(customerDTO, dataRow));
            }

            return customerDTOs;
        }

        public CustomerDTO GetById(int CustomerID)
        {
            SqlCommand getCustomer = new SqlCommand($"SELECT Customer_ID, Name, Email, Password, ShoppingCart_ID, IsAdmin " +
                                                    $"FROM Customer WHERE Customer_ID = @Customer_ID;");

            getCustomer.Parameters.AddWithValue("@Customer_ID", CustomerID);

            DataTable dataTable = database.Query(getCustomer);
            CustomerDTO customerDTO = new CustomerDTO();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                customerDTO = CustomerDTOFill(customerDTO, dataRow);
            }

            return customerDTO;
        }

        public CustomerDTO GetCustomerByOrder(int orderID)
        {
            SqlCommand getCustomer = new SqlCommand($"SELECT Customer_ID, Name, Email, Password, ShoppingCart_ID, IsAdmin " +
                                                    $"FROM Customer " +
                                                    $"INNER JOIN Order " +
                                                    $"ON Customer.Customer_ID = Order.Customer_ID " +
                                                    $"WHERE Order_ID = @Order_ID;");

            getCustomer.Parameters.AddWithValue("@Order_ID", orderID);

            DataTable dataTable = database.Query(getCustomer);

            CustomerDTO customerDTO = new CustomerDTO();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                CustomerDTOFill(customerDTO, dataRow);
            }

            return customerDTO;
        }

        public CustomerDTO GetCustomerByEmail(string email)
        {
            SqlCommand getCustomer = new SqlCommand($"SELECT Customer_ID, Name, Email, Password, ShoppingCart_ID, IsAdmin " +
                                                    $"FROM Customer WHERE Email = @Email;");

            getCustomer.Parameters.AddWithValue("@Email", email);

            DataTable dataTable = database.Query(getCustomer);
            CustomerDTO customerDTO = new CustomerDTO();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                customerDTO = CustomerDTOFill(customerDTO, dataRow);
            }

            return customerDTO;
        }

        private CustomerDTO CustomerDTOFill(CustomerDTO customerDTO, DataRow dataRow)
        {
            customerDTO.ID = Convert.ToInt32(dataRow["Customer_ID"]);
            customerDTO.Name = Convert.ToString(dataRow["Name"]);
            customerDTO.Email = Convert.ToString(dataRow["Email"]);
            customerDTO.Password = Convert.ToString(dataRow["Password"]);
            customerDTO.ShoppingCartDTO.ID = Convert.ToInt32(dataRow["ShoppingCart_ID"]);
            customerDTO.IsAdmin = Convert.ToBoolean(dataRow["IsAdmin"]);

            return customerDTO;
        }

        public void Create(CustomerDTO customerDTO)
        {
            SqlCommand instertCustomer = new SqlCommand($"INSERT INTO Customer (Name, Email, Password, ShoppingCart_ID, IsAdmin) " +
                                                        $"VALUES (@Name, @Email, @Password, @ShoppingCart_ID, @IsAdmin);");

            instertCustomer.Parameters.AddWithValue("@Name", customerDTO.Name);
            instertCustomer.Parameters.AddWithValue("@Email", customerDTO.Email);
            instertCustomer.Parameters.AddWithValue("@password", customerDTO.Password);
            instertCustomer.Parameters.AddWithValue("@ShoppingCart_ID", customerDTO.ShoppingCartDTO.ID);
            instertCustomer.Parameters.AddWithValue("@IsAdmin", customerDTO.IsAdmin);

            database.ExecuteQuery(instertCustomer);
        }

        public void Delete(CustomerDTO customerDTO)
        {
            SqlCommand deleteCustomer = new SqlCommand($"DELETE FROM Customer " +
                                                       $"WHERE Customer_ID = @Customer_ID;");

            deleteCustomer.Parameters.AddWithValue("@Customer_ID", customerDTO.ID);

            database.ExecuteQuery(deleteCustomer);
        }

        public void Update(CustomerDTO customerDTO)
        {
            SqlCommand updateCustomer = new SqlCommand($"UPDATE Customer " +
                                                       $"SET Name = @Name, Email = @Email, Password = @Password, IsAdmin = @IsAdmin " +
                                                       $"WHERE Customer_ID = @Customer_ID;");

            updateCustomer.Parameters.AddWithValue("@Name", customerDTO.Name);
            updateCustomer.Parameters.AddWithValue("@Email", customerDTO.Email);
            updateCustomer.Parameters.AddWithValue("@password", customerDTO.Password);
            updateCustomer.Parameters.AddWithValue("@IsAdmin", customerDTO.IsAdmin);
            updateCustomer.Parameters.AddWithValue("@Customer_ID", customerDTO.ID);

            database.ExecuteQuery(updateCustomer);
        }
    }
}