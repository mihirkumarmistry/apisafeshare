namespace SafeShareAPI.Provider
{
    public sealed class ConfigProvider
    {
        private const string ConfigFile = "appsettings.json";
        private static readonly Lazy<ConfigProvider> _constantsProvider = new(() => new ConfigProvider());
        public static string EncryptionKey => "S2F5dWxXSEdsdT9JZZrCkU82wOa4EwlA9VgPczysIZfo=";

    }
}
