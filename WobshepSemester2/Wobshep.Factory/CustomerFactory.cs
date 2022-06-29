using Wobshep.DAL;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.Factory
{
    public static class CustomerFactory
    {
        public static ICustomerDAL GetCustomerDAL()
        {
            return new CustomerDAL();
        }
    }
}