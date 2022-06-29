using System;
using System.Collections.Generic;
using System.Text;

namespace Wobshep.DAL.Extensions
{
    public static class ConnectionStringHelper
    {
        public static string ConnectionString => "Data Source=(Localdb)\\DBServer;Initial Catalog=Wobshep;Integrated Security=True";
    }
}
