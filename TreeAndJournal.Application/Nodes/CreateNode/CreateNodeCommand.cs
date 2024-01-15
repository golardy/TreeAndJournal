using TreeAndJournal.Application.Abstractions;

namespace TreeAndJournal.Application.Nodes.CreateNode
{
    public record CreateNodeCommand(string TreeName, int ParentNodeId, string NodeName) : ICommand;
}
