using Microsoft.EntityFrameworkCore;
using MStatistics.Data;
using MStatistics.Data.Repositories;
using MStatistics.DomainModels.Entities;


namespace MStatistics.DomainModels
{
    public class TweetRepository : GenericRepository<Tweet>, ITweetRepository
    {
        public TweetRepository(ApplicationContext context) : base(context)
        {
            
        }

        /// <inheritdoc />
        public async Task<int> GetCount()
        {
            return await _context.Tweets.CountAsync();
        }
    }
}
