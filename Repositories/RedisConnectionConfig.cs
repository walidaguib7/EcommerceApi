using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Ecommerce.Repositories
{
    public class RedisConnectionConfig
    {
        private readonly IConnectionMultiplexer connection;

        public RedisConnectionConfig(ConfigurationOptions options)
        {

            connection = ConnectionMultiplexer.Connect(options);
        }

        public IDatabase GetDatabase()
        {
            return connection.GetDatabase();
        }
    }
}