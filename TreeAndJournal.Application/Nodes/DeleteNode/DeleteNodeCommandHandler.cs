using TreeAndJournal.Application.Abstractions;
using TreeAndJournal.Application.Abstractions.Date;
using TreeAndJournal.Application.Exceptions;
using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Nodes.DeleteNode
{
    public class DeleteNodeCommandHandler : ICommandHandler<DeleteNodeCommand>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNodeCommandHandler(IDateTimeProvider dateTimeProvider,
            INodeRepository nodeRepository,
            IUnitOfWork unitOfWork)
        {
            _dateTimeProvider = dateTimeProvider;
            _nodeRepository = nodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            var node = await _nodeRepository.GetNodeByIdAsync(request.NodeId, cancellationToken);
            if (node == null)
            {
                throw new CustomValidationException(_dateTimeProvider.UtcNow.Ticks, "Node not exists");
            }

            var nodes = await _nodeRepository.GetNodesByTreeNameAsync(request.TreeName, cancellationToken);
            if (HasNodeChildren(node, nodes))
            {
                throw new CustomValidationException(_dateTimeProvider.UtcNow.Ticks, "Entity can not be deleted because of it has children");
            }

            _nodeRepository.Remove(node);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        private static bool HasNodeChildren(Node node, IEnumerable<Node> nodes)
        {
            return nodes != null && nodes.Any() && nodes.Any(x => x.ParentNodeId == node.Id);
        }
    }
}
