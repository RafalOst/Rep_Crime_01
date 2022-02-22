using CommonItems;

namespace EventBus.Messaging.Events
{
    public class UpdateCrimeEvent: BaseEvent
    {
        public string CrimeId { get; set; }
        public int? AssignedLawEnforcmentId { get; set; }
        public CrimeReportStatus CrimeReportStatus { get; set; }
    }
}
