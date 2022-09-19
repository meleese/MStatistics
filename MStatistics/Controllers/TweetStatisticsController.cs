using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MStatistics.DomainModels;
using MStatistics.DomainModels.Entities;
using System;
using System.Threading.Tasks;

namespace MStatistics.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TweetStatisticsController : ControllerBase
    {
        private readonly ILogger<TweetStatisticsController> _logger;
        private readonly ITweetRepository _tweetRepository;
        private readonly IHashTagRepository _hashTagRepository;

        public TweetStatisticsController(ILogger<TweetStatisticsController> logger, ITweetRepository tweetRepository, IHashTagRepository hashTagRepository)
        {
            _logger = logger;
            _tweetRepository = tweetRepository;
            _hashTagRepository = hashTagRepository;
        }

        /// <summary>
        /// Gets a tweet statistic model containing total tweets, hashtags and top hashtags
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet(Name = "GetTweetStatistics")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tweetCount = await _tweetRepository.GetCount();
                var hashTagCount = await _hashTagRepository.GetCount();
                var topTags = await _hashTagRepository.GetTopHashTags(10);

                return Ok(
                    new TweetStatistic()
                    {
                        TopTenHashTags = topTags,
                        TotalTweets = tweetCount,
                        TotalHashTags = hashTagCount
                    });
            }
            catch (Exception e)
            {
                _logger.LogError($"An error was encountered while executing {nameof(Get)} in {nameof(TweetStatisticsController)}. Message: {e.Message}. Inner Exception: {e.InnerException}. Stack Trace: {e.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}