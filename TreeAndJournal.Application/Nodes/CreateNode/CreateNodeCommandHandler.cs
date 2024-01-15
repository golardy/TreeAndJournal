using MediatR;
using TreeAndJournal.Application.Abstractions;
using TreeAndJournal.Application.Exceptions;
using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Nodes.CreateNode
{
    public class CreateNodeCommandHandler : ICommandHandler<CreateNodeCommand>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNodeCommandHandler(INodeRepository nodeRepository, IUnitOfWork unitOfWork)
        {
            _nodeRepository = nodeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateNodeCommand request, CancellationToken cancellationToken)
        {
            var parent = await _nodeRepository.GetNodeByIdAsync(request.ParentNodeId, cancellationToken);
            if (parent == null)
            {
                throw new CustomValidationException("Parent is not exists");
            }

            var nodes = await _nodeRepository.GetNodesByTreeNameAsync(request.TreeName, cancellationToken);
            if (nodes != null && nodes.Any() && nodes.Any(x => x.Name.Equals(request.NodeName)))
            {
                throw new CustomValidationException("Duplicate name");
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
