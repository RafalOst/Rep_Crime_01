using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Data
{
    public interface ICrimeDbContext
    {
        IMongoCollection<Crime> Crimes { get; }
    }
}

