using CrimeService.Models;

namespace CrimeService.Services
{
    public interface ICrimeRepository
    {
        Task<IEnumerable<Crime>> GetAsyncCrimes();
        Task<Crime?> GetAsyncCrime(string id);
        Task CreateAsyncCrime(Crime newCrime);
        Task UpdateAsyncCrime(string id, Crime updatedCrime);
        Task <bool> RemoveCrimeAsync(string id);
    }

}
