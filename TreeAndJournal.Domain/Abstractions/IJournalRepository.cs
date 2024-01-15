using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TreeAndJournal.Domain.Abstractions
{
    public interface IJournalRepository
    {
        Task<JournalItem> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        IQueryable<JournalItem> GetAll(CancellationToken cancellationToken = default);
        void Add(JournalItem journalItem);
    }
}
