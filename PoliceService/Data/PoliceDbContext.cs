using Microsoft.EntityFrameworkCore;
using PoliceService.Models;

namespace PoliceService.Data
{
    public class PoliceDbContext: DbContext
    {
        public PoliceDbContext(DbContextOptions<PoliceDbContext> options) : base(options)
        {

        }

        public DbSet<PoliceOfficer> PoliceOfficers { get; set; }
        public DbSet<Crime> Crimes { get; set; }
    }
}
