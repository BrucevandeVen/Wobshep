using System;
using System.Collections.Generic;
using System.Text;

namespace Wobshep.Interfaces.DTOs
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public decimal TotalPrice { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
        public List<ProductDTO> ProductDTOs { get; set; }
    }
}