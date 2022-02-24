using CommonItems;
using System.ComponentModel.DataAnnotations;

namespace PoliceService.Models
{
    public class Crime
    {
        public string Id { get; set; }

        public CrimeEventType CrimeType { get; set; }

        public string? Description { get; set; }

        public CrimeReportStatus CrimeReportStatus { get; set; }

        public string PlaceOfEvent { get; set; }

        public int? AssignedLawEnforcmentId { get; set; }

        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime CreatedDate { get; set; }
    }
}