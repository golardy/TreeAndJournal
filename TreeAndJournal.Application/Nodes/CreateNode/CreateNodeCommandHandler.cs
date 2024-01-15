using MediatR;
using TreeAndJournal.Application.Abstractions;
using TreeAndJournal.Application.Abstractions.Date;
using TreeAndJournal.Application.Exceptions;
using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Nodes.CreateNode
{
    public class CreateNodeCommandHandler : ICommandHandler<CreateNodeCommand>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNodeCommandHandler(IDateTimeProvider dateTimeProvider,
            INodeRepository nodeRepository, 
            IUnitOfWork unitOfWork)
        {
            _dateTimeProvider = dateTimeProvider;
            _nodeRepository = nodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateNodeCommand request, CancellationToken cancellationToken)
        {
            var parent = await _nodeRepository.GetNodeByIdAsync(request.ParentNodeId, cancellationToken);
            if (parent == null)
            {
                throw new CustomValidationException(_dateTimeProvider.UtcNow.Ticks, "Parent is not exists");
            }

            var nodes = await _nodeRepository.GetNodesByTreeNameAsync(request.TreeName, cancellationToken);
            if (nodes != null && nodes.Any() && nodes.Any(x => x.Name.Equals(request.NodeName)))
            {
                throw new CustomValidationException(_dateTimeProvider.UtcNow.Ticks, "Duplicate name");
            }

            _nodeRepository.Add(new Node
            {
                Name = request.NodeName,
                ParentNodeId = request.ParentNodeId
            });
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
