using CrimeService.Data;
using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Services
{
    public class CrimeRepository : ICrimeRepository
    {
        private readonly ICrimeDbContext _context;
        private readonly ILogger<CrimeRepository> _logger;

        public CrimeRepository(ICrimeDbContext crimeDbContext, ILogger<CrimeRepository> logger)
        {
            _context = crimeDbContext;
            _logger = logger;
        }

        public async Task CreateAsyncCrime(Crime crime)
        {
            _logger.LogInformation("--> Creating Crime...");
            await _context.Crimes.InsertOneAsync(crime);
        }

        public async Task<Crime?> GetAsyncCrime(string id)
        {
            var crime = await _context.Crimes.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (crime == null)
            {
                _logger.LogError($"Crime with id: {id} not found.");
            }
            return crime;
        }

        public async Task<IEnumerable<Crime>> GetAsyncCrimes()
        {
            _logger.LogInformation("--> Get Crimes...");
            return await _context.Crimes.Find(_ => true).ToListAsync();
        }

        public async Task<bool> RemoveCrimeAsync(string id)
        {
            _logger.LogInformation($"--> delete crime with id: {id}...");

            FilterDefinition<Crime> filter = Builders<Crime>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _context.Crimes.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task UpdateAsyncCrime(Crime updatedCrime)
        {
            _logger.LogInformation($"--> updating crime with id: {updatedCrime.Id}...");

            await _context.Crimes.ReplaceOneAsync(x => x.Id == updatedCrime.Id, updatedCrime);
        }
    }
}
