using MediatR;

namespace TreeAndJournal.Application.Tree.GetTree
{
    public record GetTreeQuery(string TreeName): IRequest<IEnumerable<NodeDto>>;
}
