using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FBA.Repository
{
    public class DbContext
    {
        public IMongoDatabase Database { get; }

        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;

            var connection = configuration["MongoConnection"];
            var database = configuration["MongoDatabase"];
            var mongoClient = new MongoClient(connection);
            Database = mongoClient.GetDatabase(database);
        }
    }
}