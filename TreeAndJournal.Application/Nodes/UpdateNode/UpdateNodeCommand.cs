using TreeAndJournal.Application.Abstractions;

namespace TreeAndJournal.Application.Nodes.UpdateNode
{
    public record UpdateNodeCommand(string TreeName, int NodeId, string NewNodeName): ICommand;
}
