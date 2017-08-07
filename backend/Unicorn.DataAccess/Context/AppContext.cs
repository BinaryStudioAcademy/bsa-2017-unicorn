using System.Data.Entity;

namespace Unicorn.DataAccess.Context
{
    class AppContext: DbContext
    {

        public AppContext(string connectionString)
        : base(connectionString)
        {

        }
    }

}
