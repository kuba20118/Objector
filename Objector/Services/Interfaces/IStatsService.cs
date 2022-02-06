using Objector.Models;
using Objector.Models.Charts;

namespace Objector.Services.Interfaces
{
    public interface IStatsService
    {
        Task AddStatsToImage(Guid id, Feedback stats);
        Task<IEnumerable<Statistics>> GetAllAsync();
        Task<Statistics> GetImageStatsAsync(Guid id);
        Task<SummaryStats> GetSummaryStats();
        Task UpdateGeneralStats(Guid id, Feedback stats);
    }
}
