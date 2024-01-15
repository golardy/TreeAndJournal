namespace TreeAndJournal.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public long EventId { get; set; }
        public string Error { get; }
        public CustomValidationException(long eventId, string error)
        {
            EventId = eventId;
            Error = error;
        }
    }
}
