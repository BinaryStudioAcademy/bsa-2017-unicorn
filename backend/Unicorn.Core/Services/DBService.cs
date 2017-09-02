using System.Data.Entity;
using System.Data.SqlClient;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Context;

namespace Unicorn.Core.Services
{
    public class DBService: IDBService
    {
        public void RecreateDatabase()
        {
            SqlConnection.ClearAllPools();
            Database.Delete("DefaultConnection");

            using (DbContext context = new AppContext())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}
