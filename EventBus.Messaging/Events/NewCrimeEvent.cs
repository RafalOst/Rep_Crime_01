using CommonItems;

namespace EventBus.Messaging.Events
{
    public class NewCrimeEvent: BaseEvent
    {
        public string Id { get; set; }

        public CrimeEventType CrimeType { get; set; }

        public string? Description { get; set; }

        public CrimeReportStatus CrimeReportStatus { get; set; }

        public string PlaceOfEvent { get; set; }

        public int? AssignedLawEnforcmentId { get; set; }

        public string? Email { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
