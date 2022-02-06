using Objector.Models;

namespace Objector.Repositories.Interfaces
{
    public interface IGeneralStatsRepository
    {
        Task CreateAsync();
        Task<GeneralStats> GetAsync();
        Task UpdateAsync(Feedback feedback, int numberOfObjects, IList<string> foundObjects, long time);
    }
}
