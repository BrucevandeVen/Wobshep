using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WobshepUI.Models
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ShoppingCartID { get; set; }
        public bool IsAdmin { get; set; }
    }
}
