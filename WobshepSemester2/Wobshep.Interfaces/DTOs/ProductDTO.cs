using System;
using System.Collections.Generic;
using System.Text;

namespace Wobshep.Interfaces.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
