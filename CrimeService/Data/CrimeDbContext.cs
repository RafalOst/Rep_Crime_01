using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Data
{
    public class CrimeDbContext : ICrimeDbContext
    {
        public CrimeDbContext(IConfiguration configuartion)
        {
            var client = new MongoClient(configuartion.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuartion.GetValue<string>("DatabaseSettings:DatabaseName"));

            Crimes = database.GetCollection<Crime>(configuartion.GetValue<string>("DatabaseSettings:CollectionName"));

            CrimeContextSeeder.SeedData(Crimes);
        }

        public IMongoCollection<Crime> Crimes { get; }
    }
}