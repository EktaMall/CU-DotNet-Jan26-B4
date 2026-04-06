using MongoDB.Driver;
using SmartBankFrontend.Models;

namespace SmartBankFrontend.Services
{
    public class FeedBackService
    {
        private readonly IMongoCollection<Feedback> _collection;

        public FeedBackService(IConfiguration config)
        {
            var settings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<Feedback>(settings.FeedbackCollection);
        }

        public async Task CreateAsync(Feedback feedback)
        {
            await _collection.InsertOneAsync(feedback);
        }
    }
}
