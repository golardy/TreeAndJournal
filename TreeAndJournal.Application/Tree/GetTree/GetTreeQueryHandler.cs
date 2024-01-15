using AutoMapper;
using MediatR;
using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Application.Tree.GetTree
{
    public class GetTreeQueryHandler : IRequestHandler<GetTreeQuery, IEnumerable<NodeDto>>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTreeQueryHandler(INodeRepository nodeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _nodeRepository = nodeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NodeDto>> Handle(GetTreeQuery request, CancellationToken cancellationToken)
        {
            const int rootId = 1;

            var dbItems = await _nodeRepository.GetNodesByTreeNameAsync(request.TreeName);
            if (!IsTreeExists(dbItems))
            {
                _nodeRepository.Add(new Node { Name = request.TreeName, ParentNodeId = rootId });
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return _mapper.Map<IEnumerable<NodeDto>>(new List<Node> { dbItems.First() });
            }

            var rootItem = dbItems.FirstOrDefault(x => x.Name.Equals(request.TreeName));

            return GetTree(rootItem, dbItems);
        }

        private static bool IsTreeExists(IEnumerable<Node> dbItems)
        {
            return dbItems.Any();
        }

        private IEnumerable<NodeDto> GetTree(Node rootItem, IEnumerable<Node> dbItems)
        {
            var root = new NodeDto { Id = rootItem.Id, Name = rootItem.Name };

            var children = dbItems.Where(x => x.ParentNodeId == rootItem.Id).ToList();
            if (children != null && children.Any())
            {
                foreach (var child in children)
                {
                    root.Children.AddRange(GetTree(child, dbItems));
                }
            }

            yield return root;
        }
    }
}
