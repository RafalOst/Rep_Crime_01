using Microsoft.EntityFrameworkCore;
using PoliceService.Data;
using PoliceService.Models;

namespace PoliceService.Services
{
    public class PoliceRepository : IPoliceRepository
    {
        private readonly PoliceDbContext _context;

        public PoliceRepository(PoliceDbContext context)
        {
            _context = context;
        }

        public async Task AddCrime(Crime crime)
        {
            await _context.Crimes.AddAsync(crime);
        }

        public async Task AddPoliceOfficer(PoliceOfficer policeOfficer)
        {
            await _context.PoliceOfficers.AddAsync(policeOfficer);
        }

        public async Task DeletePoliceOfficerAsync(int policeOfficerId)
        {
            var policeOfficer = await _context.PoliceOfficers.FindAsync(policeOfficerId);
            if (policeOfficer != null)
            {
                _context.PoliceOfficers.Remove(policeOfficer);
            }
        }

        public async Task<IEnumerable<Crime>> GetAllCrimesAsync()
        {
            return await _context.Crimes.ToListAsync();
        }

        public async Task<IEnumerable<Crime>> GetAllCrimesByPoliceOfficerIdAsync(int policeOfficerId)
        {
            return await _context.Crimes
            .Where(x => x.AssignedLawEnforcmentId == policeOfficerId)
            .ToListAsync();
        }

        public async Task<IEnumerable<PoliceOfficer>> GetAllPoliceOfficersAsync()
        {
            return await _context.PoliceOfficers
            .Include(x => x.Crimes)
            .ToListAsync();
        }

        public async Task<PoliceOfficer> GetPoliceOfficerByIdAsync(int policeOfficerId)
        {
            return await _context.PoliceOfficers
            .Include(x => x.Crimes)
            .FirstOrDefaultAsync(c => c.Id == policeOfficerId);
        }

        public async Task<PoliceOfficer> GetPoliceOfficerWithLowerCrimesTaskAsync()
        {
            return await _context.PoliceOfficers
            .Include(x => x.Crimes.Where(x => x.CrimeReportStatus == CommonItems.CrimeReportStatus.Waiting))
            .OrderBy(policeOfficers => policeOfficers.Crimes.Count)
            .FirstOrDefaultAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
