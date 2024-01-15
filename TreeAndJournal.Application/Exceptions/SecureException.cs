namespace TreeAndJournal.Application.Exceptions
{
    public class SecureException: Exception
    {
        public int EventId { get; }
        public SecureException(int eventId)
        {
            EventId = eventId;
        }
    }
}
