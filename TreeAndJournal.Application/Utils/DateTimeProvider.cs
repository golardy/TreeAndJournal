using TreeAndJournal.Application.Abstractions.Date;

namespace TreeAndJournal.Application.Utils
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
