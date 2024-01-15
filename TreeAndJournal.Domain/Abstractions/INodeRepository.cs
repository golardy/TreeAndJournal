using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TreeAndJournal.Domain.Abstractions
{
    public interface INodeRepository
    {
        void Add(Node entity);
        void Remove(Node entity);
        Task<IEnumerable<Node>> GetNodesByTreeNameAsync(string treeName, CancellationToken cancellationToken = default);
        Task<Node> GetNodeByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
