using CommonItems;
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
                new PoliceOfficer() { Id = 1, Name = "Szczur", Rank = PoliceRank.Inspector,
                    Crimes = new List<Crime>()
                    {
                        new Crime()
                        {
                            Id = "602d2149e773f2a3990b47f5",
                            CrimeType = CrimeEventType.Littering,
                            CrimeReportStatus = CrimeReportStatus.Waiting,
                            AssignedLawEnforcmentId = 1,
                            Description = "Obywatel Kot wyrzuca smieci na ulice",
                            PlaceOfEvent = "Krakow, ul. Kocia 7",
                            Email = "kot@kot.com"
                        }
                    }
                },
                new PoliceOfficer() { Id = 2, Name = "Chomik", Rank = PoliceRank.Detective,
                    Crimes = new List<Crime>()
                    {
                        new Crime()
                        {
                            Id = "602d2149e773f2a3990b47f4",
                            CrimeType = CrimeEventType.Assault,
                            CrimeReportStatus = CrimeReportStatus.Waiting,
                            AssignedLawEnforcmentId = 2,
                            Description = "Obywatel Kot napadl na obywatela Psa",
                            PlaceOfEvent = "Krakow, ul. Psia 6",
                            Email = "pies@pies.com"
                        }
                    }
                },
                new PoliceOfficer() { Id = 3, Name = "Mysz", Rank = PoliceRank.Constable,
                    Crimes = new List<Crime>()
                    {
                        new Crime()
                        {
                            Id = "602d2149e773f2a3990b47f3",
                            CrimeType = CrimeEventType.Burglary,
                            CrimeReportStatus = CrimeReportStatus.Waiting,
                            AssignedLawEnforcmentId = 3,
                            Description = "Obywatel Kot wlamal sie do obywatela Ptaka",
                            PlaceOfEvent = "Krakow, ul. Ptasia 3/2",
                            Email = "ptak@ptak.com"
                        }
                    }
                },
            };
        }
    }
}
