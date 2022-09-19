using MStatistics.DomainModels.Entities;

namespace MStatistics.DomainModels
{
    public interface ITweetRepository : IGenericRepository<Tweet>
    {
        /// <summary>
        /// Gets the total count of tweets
        /// </summary>
        /// <returns>Int</returns>
        Task<int> GetCount();
    }
}
