namespace TreeAndJournal.Application.Journal.GetItem
{
    public class JournalDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
