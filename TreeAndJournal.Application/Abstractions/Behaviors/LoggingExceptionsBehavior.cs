using MediatR;
using TreeAndJournal.Application.Abstractions.Date;
using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Abstractions.Behaviors
{
    public class LoggingExceptionsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly IJournalRepository _journalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LoggingExceptionsBehavior(
            IJournalRepository journalRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _journalRepository = journalRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                _journalRepository.Add(new JournalItem
                {
                    Path = name,
                    EventId = _dateTimeProvider.UtcNow.Ticks,
                    CreatedAt = _dateTimeProvider.UtcNow,
                    StackTrace = exception.StackTrace,
                    RequestParams = string.Join(',', request.GetType().Attributes)
                });

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                throw;
            }
        }
    }
}
