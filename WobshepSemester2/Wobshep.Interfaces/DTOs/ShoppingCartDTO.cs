using System;
using System.Collections.Generic;
using System.Text;

namespace Wobshep.Interfaces.DTOs
{
    public class ShoppingCartDTO
    {
        public int ID { get; set; }
        public List<ProductDTO> ProductDTOs { get; set; }
    }
}