using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WobshepUI.Models.Product
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
