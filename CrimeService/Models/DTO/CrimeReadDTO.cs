using CommonItems;

namespace CrimeService.Models
{
    public class CrimeReadDTO
    {
        public string Id { get; set; }

        public CrimeEventType CrimeType { get; set; }

        public string? Description { get; set; }

        public CrimeReportStatus CrimeReportStatus { get; set; }

        public string PlaceOfEvent { get; set; }

        public int? AssignedLawEnforcmentId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
