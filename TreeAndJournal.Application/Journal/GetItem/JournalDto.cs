namespace TreeAndJournal.Application.Journal.GetItem
{
    public class JournalDto
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
