using CommonItems;
using EventBus.Messaging.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PoliceService.Data;
using PoliceService.Models;

namespace PoliceService.Services
{
    public class PoliceRepository : IPoliceRepository
    {
        private readonly PoliceDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<PoliceRepository> _logger;

        public PoliceRepository(PoliceDbContext context, IPublishEndpoint publishEndpoint, ILogger<PoliceRepository> logger)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task AddCrime(Crime crime)
        {
            _logger.LogInformation("--> Add Crime...");
            await _context.Crimes.AddAsync(crime);
        }

        public async Task AddPoliceOfficer(PoliceOfficer policeOfficer)
        {
            _logger.LogInformation("--> Add Police Officer...");
            await _context.PoliceOfficers.AddAsync(policeOfficer);
        }

        public async Task DeletePoliceOfficerAsync(int policeOfficerId)
        {
            var policeOfficer = await _context.PoliceOfficers.FindAsync(policeOfficerId);
            if (policeOfficer != null)
            {
                _logger.LogInformation($"--> delete Police Officer with id: {policeOfficerId}");
                _context.PoliceOfficers.Remove(policeOfficer);
            }
        }

        public async Task<IEnumerable<Crime>> GetAllCrimesAsync()
        {
            _logger.LogInformation("--> Get all crimes...");
            return await _context.Crimes.ToListAsync();
        }

        public async Task<IEnumerable<Crime>> GetAllCrimesByPoliceOfficerIdAsync(int policeOfficerId)
        {
            _logger.LogInformation($"--> Get all crimes for Police Officer with id: {policeOfficerId}");
            return await _context.Crimes
            .Where(x => x.AssignedLawEnforcmentId == policeOfficerId)
            .ToListAsync();
        }

        public async Task<IEnumerable<PoliceOfficer>> GetAllPoliceOfficersAsync()
        {
            _logger.LogInformation("--> Get all Police Officers...");
            return await _context.PoliceOfficers
            .Include(x => x.Crimes)
            .ToListAsync();
        }

        public async Task<PoliceOfficer> GetPoliceOfficerByIdAsync(int policeOfficerId)
        {
            _logger.LogInformation($"--> Get Police Officers with id: {policeOfficerId}");
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

        public async Task UpdateCrimeStatusAsync(string crimeId, CrimeReportStatus newStatus)
        {
            _logger.LogInformation($"--> Update crime with id: {crimeId}");
            var crimeToUpdate = await _context.Crimes.FindAsync(crimeId);
            crimeToUpdate.CrimeReportStatus = newStatus;
            _context.SaveChanges();

            try
            {
                var eventMessage = new UpdateCrimeEvent
                {
                    CrimeId = crimeToUpdate.Id,
                    AssignedLawEnforcmentId = crimeToUpdate.AssignedLawEnforcmentId,
                    CrimeReportStatus = crimeToUpdate.CrimeReportStatus
                };
                await _publishEndpoint.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }
        }
    }
}
