using System;
using System.Collections.Generic;
using System.Text;
using Wobshep.DAL;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.Factory
{
    public static class ProductFactory
    {
        public static IProductDAL GetProductDAL()
        {
            return new ProductDAL();
        }
    }
}