using System.Configuration;

namespace Unicorn.Core.Infrastructure
{
    public class AdminAuthConfig : ConfigurationSection
    {
        public static AdminAuthConfig Config => config;

        [ConfigurationProperty("login", DefaultValue = "admin", IsRequired = false)]
        public string Login => (string)this["login"];

        [ConfigurationProperty("password", DefaultValue = "admin", IsRequired = false)]
        public string Password => (string)this["password"];

        private static AdminAuthConfig config = ConfigurationManager.GetSection("AdminAuthConfig") as AdminAuthConfig;
    }
}
