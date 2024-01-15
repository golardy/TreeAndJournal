namespace TreeAndJournal.Application.Abstractions.Date
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
