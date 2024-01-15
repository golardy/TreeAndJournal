using TreeAndJournal.Application.Journal.GetItem;

namespace TreeAndJournal.Application.Journal.GetFilteredItems
{
    public class FilteredJournalDto
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<JournalDto> Items { get; set; }
    }
}
