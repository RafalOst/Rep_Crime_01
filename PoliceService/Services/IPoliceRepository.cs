using PoliceService.Models;

namespace PoliceService.Services
{
    public interface IPoliceRepository
    {
        Task<IEnumerable<PoliceOfficer>> GetAllPoliceOfficersAsync();
        Task<PoliceOfficer> GetPoliceOfficerByIdAsync(int policeOfficerId);
        Task AddCrime(Crime crime);
        Task AddPoliceOfficer(PoliceOfficer policeOfficer);
        Task DeletePoliceOfficerAsync(int policeOfficerId);
        Task<IEnumerable<Crime>> GetAllCrimesByPoliceOfficerIdAsync(int policeOfficerId);
        Task SaveChanges();
        Task<PoliceOfficer> GetPoliceOfficerWithLowerCrimesTaskAsync();
        Task<IEnumerable<Crime>> GetAllCrimesAsync();
    }
}
