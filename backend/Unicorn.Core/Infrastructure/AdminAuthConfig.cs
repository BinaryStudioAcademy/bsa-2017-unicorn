using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.Infrastructure
{
    public class AdminAuthConfig : ConfigurationSection
    {
        public static AdminAuthConfig Config
        {
            get
            {
                return config;
            }
        }

        [ConfigurationProperty("login", DefaultValue = "admin", IsRequired = false)]
        public string Login
        {
            get
            {
                return (string)this["login"];
            }
        }

        [ConfigurationProperty("password", DefaultValue = "admin", IsRequired = false)]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
        }

        private static AdminAuthConfig config = ConfigurationManager.GetSection("AdminAuthConfig") as AdminAuthConfig;
    }
}
