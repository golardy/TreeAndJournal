using Microsoft.EntityFrameworkCore;
using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Infrastructure.Repositories
{
    public class NodeRepository : INodeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NodeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Node>> GetNodesByTreeNameAsync(string treeName, CancellationToken cancellationToken)
        {
            return await _dbContext.Node
                .FromSqlRaw<Node>($"" +
                    $"WITH RECURSIVE rectree AS " +
                    $"(SELECT \"Id\", \"Name\", \"ParentNodeId\" " +
                    $"FROM public.\"Node\" c " +
                    $"WHERE c.\"Name\" = '{treeName}' " +
                    $"UNION ALL " +
                    $"SELECT t.\"Id\", t.\"Name\", t.\"ParentNodeId\" " +
                    $"FROM public.\"Node\" t " +
                    $"JOIN rectree " +
                    $"ON t.\"ParentNodeId\" = rectree.\"Id\") " +
                    $"SELECT \"Id\", \"Name\", \"ParentNodeId\" " +
                    $"FROM rectree;")
                .ToListAsync(cancellationToken);
        }

        public void Add(Node entity)
        {
            _dbContext.Add(entity);
        }

        public async Task<Node> GetNodeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.FindAsync<Node>(id, cancellationToken);
        }

        public void Remove(Node entity)
        {
            _dbContext.Node.Remove(entity);
        }
    }
}
