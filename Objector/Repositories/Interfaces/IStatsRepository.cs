using Objector.Models;

namespace Objector.Repositories.Interfaces
{
    public interface IStatsRepository
    {
        Task AddAsync(Statistics statistics);
        Task<IEnumerable<Statistics>> GetAllAsync();
        Task<Statistics> GetAsync(Guid id);
    }
}
