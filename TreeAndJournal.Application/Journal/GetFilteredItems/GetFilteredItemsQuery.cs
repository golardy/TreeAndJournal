using MediatR;

namespace TreeAndJournal.Application.Journal.GetFilteredItems
{
    public record class GetFilteredItemsQuery(
        int Skip,
        int Take,
        DateTime? From,
        DateTime? To,
        string Search): IRequest<FilteredJournalDto>;
}
