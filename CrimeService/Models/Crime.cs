using CommonItems;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CrimeService.Models
{
    public class Crime
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public CrimeEventType CrimeType { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public CrimeReportStatus CrimeReportStatus { get; set; } = CrimeReportStatus.Waiting;

        [MinLength(10)]
        [MaxLength(250)]
        public string PlaceOfEvent { get; set; }

        public int? AssignedLawEnforcmentId { get; set; }

        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}