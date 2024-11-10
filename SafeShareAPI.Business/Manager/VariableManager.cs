using SafeShareAPI.Model;
using static SafeShareAPI.Provider.ConnectionProvider;

namespace SafeShareAPI.Business
{
    public class VariableManager : IDisposable
    {
        public User User { get; set; }
        public void Dispose() { GC.SuppressFinalize(this); }
        public Connection GetConnection() { return GetProvider.GetConnection(); }
    }
}
