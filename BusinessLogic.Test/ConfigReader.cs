using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Test
{
    public class ConfigReader
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }
    }
}
