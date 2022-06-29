﻿using System;
using System.Collections.Generic;
using System.Text;
using Wobshep.DAL;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.Factory
{
    public static class ShoppingCartFactory
    {
        public static IShoppingCartDAL GetShoppingCartDAL()
        {
            return new ShoppingCartDAL();
        }
    }
}