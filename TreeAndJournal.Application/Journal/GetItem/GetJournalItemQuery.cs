using MediatR;

namespace TreeAndJournal.Application.Journal.GetItem
{
    public record GetJournalItemQuery(int Id) : IRequest<JournalDto>;
}
