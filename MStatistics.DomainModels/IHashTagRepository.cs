using MStatistics.DomainModels.Entities;

namespace MStatistics.DomainModels
{
    public interface IHashTagRepository : IGenericRepository<HashTag>
    {
        /// <summary>
        /// Gets the total count of hasg tags
        /// </summary>
        /// <returns>Int</returns>
        Task<int> GetCount();

        /// <summary>
        /// Gets the top hashtags from the db context
        /// </summary>
        /// <param name="count">Number of top hash tags to return</param>
        /// <returns>IEnumerable<string></string></returns>
        Task<IEnumerable<string>> GetTopHashTags(int count);
    }
}
