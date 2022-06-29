using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WobshepUI.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
