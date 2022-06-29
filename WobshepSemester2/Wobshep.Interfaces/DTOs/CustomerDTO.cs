using System;
using System.Collections.Generic;
using System.Text;

namespace Wobshep.Interfaces.DTOs
{
    public class CustomerDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ShoppingCartDTO ShoppingCartDTO { get; set; } = new ShoppingCartDTO();
        public bool IsAdmin { get; set; }
    }
}