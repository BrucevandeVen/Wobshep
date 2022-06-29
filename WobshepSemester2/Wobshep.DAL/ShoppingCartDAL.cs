using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Wobshep.Interfaces.DTOs;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.DAL
{
    public class ShoppingCartDAL : IShoppingCartDAL
    {
        private DatabaseDAL database = new DatabaseDAL();

        public List<ShoppingCartDTO> GetAll()
        {
            SqlCommand getShoppingCarts = new SqlCommand($"SELECT ShoppingCart_ID " +
                                                         $"FROM ShoppingCarts;");

            DataTable dataTable = database.Query(getShoppingCarts);

            List<ShoppingCartDTO> shoppingCartDTOs = new List<ShoppingCartDTO>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO();
                ShoppingCartDTOFill(shoppingCartDTO, dataRow);
                shoppingCartDTOs.Add(shoppingCartDTO);
            }

            return shoppingCartDTOs;
        }

        public ShoppingCartDTO GetByCustomerID(int customerID)
        {
            SqlCommand getShoppingCart = new SqlCommand($"SELECT ShoppingCart_ID " +
                                                        $"FROM Customer " +
                                                        $"WHERE Customer_ID = @Customer_ID;");

            getShoppingCart.Parameters.AddWithValue("@Customer_ID", customerID);

            DataTable dataTable = database.Query(getShoppingCart);

            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                shoppingCartDTO = ShoppingCartDTOFill(shoppingCartDTO, dataRow);
            }

            return shoppingCartDTO;
        }

        public int GetLastShoppingCartID()
        {
            SqlCommand getShoppingCart = new SqlCommand($"SELECT TOP 1 ShoppingCart_ID " +
                                                        $"FROM ShoppingCart " +
                                                        $"ORDER BY ShoppingCart_ID DESC;");

            DataTable dataTable = database.Query(getShoppingCart);

            ShoppingCartDTO shoppingCartDTO = new ShoppingCartDTO();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                shoppingCartDTO.ID = Convert.ToInt32(dataRow["ShoppingCart_ID"]);
            }

            return shoppingCartDTO.ID;
        }

        private List<ProductDTO> GetProductDTOs(ShoppingCartDTO shoppingCartDTO)
        {
            SqlCommand getProducts = new SqlCommand($"SELECT Product.Product_ID, Name, Price, Description " +
                                                    $"FROM ShoppingCart_Product " +
                                                    $"INNER JOIN Product " +
                                                    $"ON ShoppingCart_Product.Product_ID = Product.Product_ID " +
                                                    $"WHERE ShoppingCart_ID = @ShoppingCart_ID;");

            getProducts.Parameters.AddWithValue("@ShoppingCart_ID", shoppingCartDTO.ID);

            DataTable dataTable = database.Query(getProducts);

            shoppingCartDTO.ProductDTOs = new List<ProductDTO>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.ID = Convert.ToInt32(dataRow["Product_ID"]);
                productDTO.Name = Convert.ToString(dataRow["Name"]);
                productDTO.Price = Convert.ToDecimal(dataRow["Price"]);
                productDTO.Description = Convert.ToString(dataRow["Description"]);
                shoppingCartDTO.ProductDTOs.Add(productDTO);
            }

            return shoppingCartDTO.ProductDTOs;
        }

        private ShoppingCartDTO ShoppingCartDTOFill(ShoppingCartDTO shoppingCartDTO, DataRow dataRow)
        {
            shoppingCartDTO.ID = Convert.ToInt32(dataRow["ShoppingCart_ID"]);
            shoppingCartDTO.ProductDTOs = GetProductDTOs(shoppingCartDTO);

            return shoppingCartDTO;
        }

        public void Create()
        {
            SqlCommand createShoppingCart = new SqlCommand($"INSERT INTO ShoppingCart " +
                                                           $"DEFAULT VALUES;");

            database.ExecuteQuery(createShoppingCart);
        }

        public void Delete(ShoppingCartDTO shoppingCartDTO)
        {
            SqlCommand deleteShoppingCart = new SqlCommand($"DELETE FROM ShoppingCart " +
                                                           $"WHERE ShoppingCart_ID = @ShoppingCart_ID;");

            deleteShoppingCart.Parameters.AddWithValue("@ShoppingCart_ID", shoppingCartDTO.ID);

            database.ExecuteQuery(deleteShoppingCart);
        }

        public void DeleteAllProducts(ShoppingCartDTO shoppingCartDTO)
        {
            SqlCommand deleteProducts = new SqlCommand($"DELETE FROM ShoppingCart_Product " +
                                                       $"WHERE ShoppingCart_ID = @ShoppingCart_ID;");

            deleteProducts.Parameters.AddWithValue("@ShoppingCart_ID", shoppingCartDTO.ID);

            database.ExecuteQuery(deleteProducts);
        }

        public void DeleteProduct(int shoppingCartID, int productID)
        {
            SqlCommand deleteProduct = new SqlCommand($"DELETE FROM ShoppingCart_Product " +
                                                      $"WHERE ShoppingCart_ID = @ShoppingCart_ID " +
                                                      $"AND Product_ID = @Product_ID;");

            deleteProduct.Parameters.AddWithValue("@ShoppingCart_ID", shoppingCartID);
            deleteProduct.Parameters.AddWithValue("@Product_ID", productID);

            database.ExecuteQuery(deleteProduct);
        }

        public void AddProduct(int shoppingCartID, int productID)
        {
            SqlCommand insertProduct = new SqlCommand($"INSERT INTO ShoppingCart_Product (ShoppingCart_ID, Product_ID) " +
                                                      $"VALUES (@ShoppingCart_ID, @Product_ID);");

            insertProduct.Parameters.AddWithValue("@Product_ID", productID);
            insertProduct.Parameters.AddWithValue("@ShoppingCart_ID", shoppingCartID);

            database.ExecuteQuery(insertProduct);
        }
    }
}