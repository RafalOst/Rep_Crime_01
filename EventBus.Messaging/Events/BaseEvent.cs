namespace EventBus.Messaging.Events
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public BaseEvent(Guid id, DateTime createDate)
        {
            EventId = id;
            CreationDate = createDate;
        }

        public Guid EventId { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
