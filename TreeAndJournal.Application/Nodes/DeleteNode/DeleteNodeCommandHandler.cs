using TreeAndJournal.Application.Abstractions;
using TreeAndJournal.Application.Exceptions;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Nodes.DeleteNode
{
    public class DeleteNodeCommandHandler : ICommandHandler<DeleteNodeCommand>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNodeCommandHandler(INodeRepository nodeRepository, IUnitOfWork unitOfWork)
        {
            _nodeRepository = nodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            var node = await _nodeRepository.GetNodeByIdAsync(request.NodeId, cancellationToken);
            if (node == null)
            {
                throw new CustomValidationException("Node not exists");
            }

            var nodes = await _nodeRepository.GetNodesByTreeNameAsync(request.TreeName, cancellationToken);
            if (nodes != null && nodes.Any() && nodes.Any(x => x.ParentNodeId == node.Id))
            {
                throw new CustomValidationException("Entity can not be deleted because of it has children");
            }

            _nodeRepository.Remove(node);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
