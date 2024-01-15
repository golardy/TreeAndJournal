using AutoMapper;
using MediatR;
using TreeAndJournal.Application.Journal.GetItem;
using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Journal.GetFilteredItems
{
    public class GetFilteredItemsQueryHandler : IRequestHandler<GetFilteredItemsQuery, FilteredJournalDto>
    {
        private readonly IJournalRepository _journalRepository;
        private readonly IMapper _mapper;

        public GetFilteredItemsQueryHandler(IJournalRepository journalRepository,
            IMapper mapper)
        {
            _journalRepository = journalRepository;
            _mapper = mapper;
        }

        public async Task<FilteredJournalDto> Handle(GetFilteredItemsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<JournalItem> journalItems = _journalRepository.GetAll(cancellationToken);

            if (!string.IsNullOrEmpty(request.Search))
            {
                journalItems = journalItems.Where(x => x.StackTrace.Contains(request.Search));
            }

            if (request.From.HasValue)
            {
                journalItems = journalItems.Where(x => x.CreatedAt > request.From.Value);
            }

            if (request.To.HasValue)
            {
                journalItems = journalItems.Where(x => x.CreatedAt < request.To.Value);
            }

            var result = journalItems
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            var mappedJournalItems = _mapper.Map<List<JournalDto>>(result);

            return new FilteredJournalDto 
            {
                Skip = request.Skip,
                Take = request.Take,
                Items = mappedJournalItems
            };
        }
    }
}
