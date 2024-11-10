using Microsoft.EntityFrameworkCore;
using static SafeShareAPI.Provider.ConnectionProvider;

namespace SafeShareAPI.Data
{
    public class SqlServerContext : ContextTables
    {
        public SqlServerContext() : base() { Connection = new Connection { Server = "MIHIRKUMAR\\SQLEXPRESS", Database = "Safeshare" }; }
        public SqlServerContext(Connection connections) : base() { Connection = connections; }
        public SqlServerContext(Connection connections, bool isMigrate) : base() { Connection = connections; if (isMigrate) { Database.Migrate(); } }
    }
}

//Add-Migration FirstMigration -Context SqlServerContext
//Add-Migration SecondMigration -Context SqlServerContext

//Update-Database -Context SqlServerContext
