using TreeAndJournal.Application.Abstractions;

namespace TreeAndJournal.Application.Nodes.DeleteNode
{
    public record DeleteNodeCommand(string TreeName, int NodeId) : ICommand;
}
