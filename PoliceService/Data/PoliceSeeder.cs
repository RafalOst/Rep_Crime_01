using Microsoft.EntityFrameworkCore;
using PoliceService.Models;

namespace PoliceService.Data
{
    public static class PoliceSeeder
    {
        public static void Initialize()
        {
            var options = new DbContextOptionsBuilder<PoliceDbContext>()
                .UseInMemoryDatabase("PoliceDb")
                .Options;

            using (var context = new PoliceDbContext(options))
            {
                if (!context.PoliceOfficers.Any())
                {
                    context.PoliceOfficers.AddRange(GetPoliceOfficers());
                    context.SaveChanges();
                }
            }
        }

        private static IEnumerable<PoliceOfficer> GetPoliceOfficers()
        {
            return new List<PoliceOfficer>() 
            { 
                new PoliceOfficer() { Name = "Szczur", Rank = PoliceRank.Inspector},
                new PoliceOfficer() { Name = "Chomik", Rank = PoliceRank.Detective},
                new PoliceOfficer() { Name = "Mysz", Rank = PoliceRank.Constable}
            };
        }

    }
}
