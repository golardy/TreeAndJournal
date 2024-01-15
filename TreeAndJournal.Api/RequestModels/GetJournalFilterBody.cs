namespace TreeAndJournal.Api.RequestModels
{
    public class GetJournalFilterBody
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Search { get; set; }
    }
}
