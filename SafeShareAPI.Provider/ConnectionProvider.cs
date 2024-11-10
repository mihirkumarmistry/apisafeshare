using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SafeShareAPI.Provider
{
    public sealed class ConnectionProvider
    {
        private static readonly Lazy<ConnectionProvider> _connectionProvider = new(() => new ConnectionProvider());
        public static ConnectionProvider GetProvider => _connectionProvider.Value;
        public Connection GetConnection()
        {
            Connection connection = new()
            {
                Id = 1,
                Server = "MIHIRKUMAR\\SQLEXPRESS",
                Database = "Safeshare",
                User = "MIHIRKUMAR\\mihir",
                Key = ""
            };
            return connection;
        }

        public class Connection
        {
            [Key] public int Id { get; set; }
            [StringLength(100)] public string Server { get; set; }
            public int? Port { get; set; }
            [StringLength(100)] public string Database { get; set; }
            [StringLength(100)] public string User { get; set; }
            [StringLength(100)] public string Key { get; set; }
        }
        public class ContextProvider : DbContext
        {
  
            public static Connection Connection { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(ConnectionBuilder.GetMsSqlConnection(Connection));
                base.OnConfiguring(optionsBuilder);
            }
            internal class ConnectionContext : ContextProvider
            {
                public ConnectionContext(Connection connections) : base() { Connection = connections; Database.EnsureCreated(); }
                public DbSet<Connection> Connections { get; set; }
            }
            public static class ConnectionBuilder
            {
                public static string GetMsSqlConnection(Connection connection)
                {
                    SqlConnectionStringBuilder connectionBuilder = new()
                    {
                        DataSource = connection.Server,
                        InitialCatalog = connection.Database,
                        ConnectTimeout = 0,
                    };

                    if (string.IsNullOrWhiteSpace(connection.User) && string.IsNullOrWhiteSpace(connection.Key))
                    {
                        connectionBuilder.IntegratedSecurity = true;
                        connectionBuilder.TrustServerCertificate = true;
                    }
                    else
                    {
                        connectionBuilder.UserID = connection.User;
                        connectionBuilder.Password = connection.Key;
                        connectionBuilder.IntegratedSecurity = true;
                        connectionBuilder.TrustServerCertificate = true;
                    }
                    return connectionBuilder.ConnectionString;
                }
            }
        }
    }
}
