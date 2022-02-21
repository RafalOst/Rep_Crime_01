using CrimeService.Data;
using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Services
{
    public class CrimeRepository : ICrimeRepository
    {
        private readonly ICrimeDbContext _context;

        public CrimeRepository(ICrimeDbContext crimeDbContext)
        {
            _context = crimeDbContext;
        }

        public async Task CreateAsyncCrime(Crime crime)
        {
            await _context.Crimes.InsertOneAsync(crime);
        }

        public async Task<Crime?> GetAsyncCrime(string id)
        {
            return await _context.Crimes.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Crime>> GetAsyncCrimes()
        {
            return await _context.Crimes.Find(_ => true).ToListAsync();
        }

        public async Task RemoveCrimeAsync(string id)
        {
            await _context.Crimes.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateAsyncCrime(string id, Crime updatedCrime)
        {
            await _context.Crimes.ReplaceOneAsync(x => x.Id == id, updatedCrime);
        }
    }
}
