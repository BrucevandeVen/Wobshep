using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Wobshep.Interfaces.DTOs;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.DAL
{
    public class ProductDAL : IProductDAL
    {
        private DatabaseDAL database = new DatabaseDAL();

        public List<ProductDTO> GetALL()
        {
            SqlCommand getProducts = new SqlCommand($"SELECT Product_ID, Name, Price, Description, ImageURL " +
                                                    $"FROM Product;");

            DataTable dataTable = database.Query(getProducts);
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ProductDTO productDTO = new ProductDTO();
                productDTOs.Add(ProductDTOFill(productDTO, dataRow));
            }

            return productDTOs;
        }

        public ProductDTO GetByID(int productID)
        {
            SqlCommand getProduct = new SqlCommand($"SELECT Product_ID, Name, Price, Description, ImageURL " +
                                                   $"FROM Product " +
                                                   $"WHERE Product_ID = @Product_ID;");

            getProduct.Parameters.AddWithValue("@Product_ID", productID);

            DataTable dataTable = database.Query(getProduct);
            ProductDTO productDTO = new ProductDTO();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                productDTO = ProductDTOFill(productDTO, dataRow);
            }

            return productDTO;
        }

        private ProductDTO ProductDTOFill(ProductDTO productDTO, DataRow dataRow)
        {
            productDTO.ID = Convert.ToInt32(dataRow["Product_ID"]);
            productDTO.Name = Convert.ToString(dataRow["Name"]);
            productDTO.Price = Convert.ToDecimal(dataRow["Price"]);
            productDTO.Description = Convert.ToString(dataRow["Description"]);
            productDTO.ImageURL = Convert.ToString(dataRow["ImageURL"]);

            return productDTO;
        }

        public void Create(ProductDTO productDTO)
        {
            SqlCommand insertProduct = new SqlCommand($"INSERT INTO Product (Name, Price, Description, ImageURL) " +
                                                      $"VALUES (@Name, @Price, @Description, @ImageURL);");

            insertProduct.Parameters.AddWithValue("@Name", productDTO.Name);
            insertProduct.Parameters.AddWithValue("@Price", productDTO.Price);
            insertProduct.Parameters.AddWithValue("@Description", productDTO.Description);
            insertProduct.Parameters.AddWithValue("@ImageURL", productDTO.ImageURL);

            database.ExecuteQuery(insertProduct);
        }

        public void Delete(ProductDTO productDTO)
        {
            SqlCommand deleteProduct = new SqlCommand($"DELETE FROM Product " +
                                                      $"WHERE Product_ID = @Product_ID;");

            deleteProduct.Parameters.AddWithValue("@Product_ID", productDTO.ID);

            database.ExecuteQuery(deleteProduct);
        }

        public void Update(ProductDTO productDTO)
        {
            SqlCommand updateProduct = new SqlCommand($"UPDATE Product " +
                                                      $"SET Name = @Name, Price = @Price, Description = @Description, ImageURL = @ImageURL " +
                                                      $"WHERE Product_ID = @Product_ID;");

            updateProduct.Parameters.AddWithValue("@Name", productDTO.Name);
            updateProduct.Parameters.AddWithValue("@Price", productDTO.Price);
            updateProduct.Parameters.AddWithValue("@Description", productDTO.Description);
            updateProduct.Parameters.AddWithValue("@ImageURL", productDTO.ImageURL);
            updateProduct.Parameters.AddWithValue("@Product_ID", productDTO.ID);

            database.ExecuteQuery(updateProduct);
        }
    }
}