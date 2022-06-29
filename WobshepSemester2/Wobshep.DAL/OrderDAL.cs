using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Wobshep.Interfaces.DTOs;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.DAL
{
    public class OrderDAL : IOrderDAL
    {
        private DatabaseDAL database = new DatabaseDAL();

        public List<OrderDTO> GetAll()
        {
            SqlCommand getOrders = new SqlCommand($"SELECT Order_ID, TotalPrice, Customer_ID " +
                                                  $"FROM Order");

            DataTable dataTable = database.Query(getOrders);

            List<OrderDTO> orderDTOs = new List<OrderDTO>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTOs.Add(OrderDTOFill(orderDTO, dataRow));
            }

            return orderDTOs;
        }

        public OrderDTO GetByCustomer(int customerID)
        {
            SqlCommand getOrder = new SqlCommand($"SELECT Order_ID, TotalPrice, Customer_ID" +
                                                 $"FROM Order " +
                                                 $"WHERE Customer_ID = @Customer_ID;");

            getOrder.Parameters.AddWithValue("@Customer_ID", customerID);

            DataTable dataTable = database.Query(getOrder);

            OrderDTO orderDTO = new OrderDTO();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderDTO = OrderDTOFill(orderDTO, dataRow);
            }

            return orderDTO;
        }

        private OrderDTO OrderDTOFill(OrderDTO orderDTO, DataRow dataRow)
        {
            orderDTO.ID = Convert.ToInt32(dataRow["Order_ID"]);
            orderDTO.TotalPrice = Convert.ToInt32(dataRow["TotalPrice"]);
            orderDTO.CustomerDTO.ID = Convert.ToInt32(dataRow["Customer_ID"]);
            orderDTO.ProductDTOs = GetProductDTOs(orderDTO);

            return orderDTO;
        }

        private List<ProductDTO> GetProductDTOs(OrderDTO orderDTO)
        {
            SqlCommand getProducts = new SqlCommand($"SELECT Product_ID, Name, Price, Description " +
                                                    $"FROM Product " +
                                                    $"INNER JOIN Order_Product " +
                                                    $"ON Product.Product_ID = Order_Product.Product_ID " +
                                                    $"WHERE Order_ID = @Order_ID;");

            getProducts.Parameters.AddWithValue("@Order_ID", orderDTO.ID);

            DataTable dataTable = database.Query(getProducts);

            orderDTO.ProductDTOs = new List<ProductDTO>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.ID = Convert.ToInt32(dataRow["Product_ID"]);
                productDTO.Name = Convert.ToString(dataRow["Name"]);
                productDTO.Price = Convert.ToDecimal(dataRow["Price"]);
                productDTO.Description = Convert.ToString(dataRow["Description"]);
                orderDTO.ProductDTOs.Add(productDTO);
            }

            return orderDTO.ProductDTOs;
        }

        public void Create(OrderDTO orderDTO)
        {
            SqlCommand insertOrder = new SqlCommand($"INSERT INTO Order (TotalPrice, Customer_ID) " +
                                                    $"VALUES (@TotalPrice, @Customer_ID);");

            insertOrder.Parameters.AddWithValue("@TotalPrice", orderDTO.TotalPrice);
            insertOrder.Parameters.AddWithValue("@Customer_ID", orderDTO.CustomerDTO.ID);

            database.ExecuteQuery(insertOrder);

            foreach (ProductDTO productDTO in orderDTO.ProductDTOs)
            {
                SqlCommand insertProduct = new SqlCommand($"INSERT INTO Order_Product (Product_ID) " +
                                                          $"VALUES (@Product_ID) " +
                                                          $"WHERE Order_ID = @Order_ID;");

                insertProduct.Parameters.AddWithValue("@Product_ID", productDTO.ID);
                insertProduct.Parameters.AddWithValue("@Order_ID", orderDTO.ID);

                database.ExecuteQuery(insertProduct);
            }
        }

        public void Delete(OrderDTO orderDTO)
        {
            SqlCommand deleteOrder = new SqlCommand($"DELETE FROM Order " +
                                                    $"WHERE Order_ID = @Order_ID;");

            deleteOrder.Parameters.AddWithValue("@Order_ID", orderDTO.ID);

            database.ExecuteQuery(deleteOrder);
        }

        public void Update(OrderDTO orderDTO)
        {
            SqlCommand updateOrder = new SqlCommand($"UPDATE Order " +
                                                    $"SET TotalPrice = @TotalPrice " +
                                                    $"WHERE Order_ID = @Order_ID;");

            updateOrder.Parameters.AddWithValue("@TotalPrice", orderDTO.TotalPrice);
            updateOrder.Parameters.AddWithValue("@Order_ID", orderDTO.ID);

            database.ExecuteQuery(updateOrder);
        }

        public void AddProduct(OrderDTO orderDTO, ProductDTO productDTO)
        {
            SqlCommand insertProduct = new SqlCommand($"INSERT INTO Order_Product (Product_ID) " +
                                                      $"VALUES (@Product_ID) " +
                                                      $"WHERE Order_ID = @Order_ID;");

            insertProduct.Parameters.AddWithValue("@Product_ID", productDTO.ID);
            insertProduct.Parameters.AddWithValue("@Order_ID", orderDTO.ID);

            database.ExecuteQuery(insertProduct);
        }

        public void DeleteProduct(OrderDTO orderDTO, ProductDTO productDTO)
        {
            SqlCommand deleteProduct = new SqlCommand($"DELETE FROM Order_Product " +
                                                      $"WHERE Order_ID = @Order_ID " +
                                                      $"AND Product_ID = @Product_ID;");

            deleteProduct.Parameters.AddWithValue("@Order_ID", orderDTO.ID);
            deleteProduct.Parameters.AddWithValue("@Product_ID", productDTO.ID);

            database.ExecuteQuery(deleteProduct);
        }
    }
}
