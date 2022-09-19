using MStatistics.DomainModels.Entities;
using MStatistics.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace MStatistics.Data.Repositories
{
    public class HashTagRepository : GenericRepository<HashTag>, IHashTagRepository
    {
        public HashTagRepository(ApplicationContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task<int> GetCount()
        {
            return await _context.HashTags.CountAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetTopHashTags(int count)
        {
            return await _context.HashTags
                .Include(t => t.Tweets)
                .OrderByDescending(t => t.Tweets.Count())
                .Select(t => t.Id)
                .Take(count).ToListAsync();
        }
    }
}
 