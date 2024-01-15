using TreeAndJournal.Domain;
using TreeAndJournal.Domain.Abstractions;

namespace TreeAndJournal.Infrastructure.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public JournalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(JournalItem journalItem)
        {
            _dbContext.Add(journalItem);
        }

        public IQueryable<JournalItem> GetAll(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<JournalItem>().AsQueryable();
        }

        public async Task<JournalItem> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<JournalItem>().FindAsync(id, cancellationToken);
        }
    }
}
