using TreeAndJournal.Application.Abstractions;
using TreeAndJournal.Application.Abstractions.Date;
using TreeAndJournal.Application.Exceptions;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Nodes.UpdateNode
{
    public class UpdateNodeCommandHandler : ICommandHandler<UpdateNodeCommand>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNodeCommandHandler(IDateTimeProvider dateTimeProvider,
            INodeRepository nodeRepository, 
            IUnitOfWork unitOfWork)
        {
            _dateTimeProvider = dateTimeProvider;
            _nodeRepository = nodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
        {
            var dbItem = await _nodeRepository.GetNodeByIdAsync(request.NodeId, cancellationToken);
            if (dbItem == null)
            {
                throw new CustomValidationException(_dateTimeProvider.UtcNow.Ticks, "Node not exists");
            }

            dbItem.Name = request.NewNodeName;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
