using CommonItems;
using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Data
{
    public class CrimeContextSeeder
    {
        public static void SeedData(IMongoCollection<Crime> crimes)
        {
            bool existCrime = crimes.Find(p => true).Any();

            if (!existCrime)
            {
                crimes.InsertManyAsync(GetPreconfiguredCrimes());
            }
        }

        private static IEnumerable<Crime> GetPreconfiguredCrimes()
        {
            return new List<Crime>()
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
                },
                 new Crime()
                {
                    Id = "602d2149e773f2a3990b47f4",
                    CrimeType = CrimeEventType.Assault,
                    CrimeReportStatus = CrimeReportStatus.Waiting,
                    AssignedLawEnforcmentId = 1,
                    Description = "Obywatel Kot napadl na obywatela Psa",
                    PlaceOfEvent = "Krakow, ul. Psia 6",
                    Email = "pies@pies.com"
                },
                  new Crime()
                {
                    Id = "602d2149e773f2a3990b47f3",
                    CrimeType = CrimeEventType.Burglary,
                    CrimeReportStatus = CrimeReportStatus.Waiting,
                    AssignedLawEnforcmentId = 1,
                    Description = "Obywatel Kot wlamal sie do obywatela Ptaka",
                    PlaceOfEvent = "Krakow, ul. Ptasia 3/2",
                    Email = "ptak@ptak.com"
                }
            };
        }
    }
}