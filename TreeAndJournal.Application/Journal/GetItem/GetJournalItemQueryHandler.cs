using AutoMapper;
using MediatR;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Journal.GetItem
{
    public class GetJournalItemQueryHandler : IRequestHandler<GetJournalItemQuery, JournalDto>
    {
        private readonly IJournalRepository _journalRepository;
        private readonly IMapper _mapper;

        public GetJournalItemQueryHandler(IJournalRepository journalRepository,
            IMapper mapper)
        {
            _journalRepository = journalRepository;
            _mapper = mapper;
        }

        public async Task<JournalDto> Handle(GetJournalItemQuery request, CancellationToken cancellationToken)
        {
            var journalItem = await _journalRepository.GetByIdAsync(request.Id, cancellationToken);

            return _mapper.Map<JournalDto>(journalItem);
        }
    }
}
