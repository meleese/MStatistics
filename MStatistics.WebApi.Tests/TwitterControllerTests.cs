using MStatistics.DomainModels;
using MStatistics.WebApi.Controllers;
using NSubstitute;
using Microsoft.Extensions.Logging.Abstractions;
using MStatistics.DomainModels.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MStatistics.WebApi.Tests
{
    [TestClass]
    public class TwitterControllerTests
    {
        private readonly TweetStatisticsController _controller;
        private readonly ITweetRepository _tweetRepositorySub;
        private readonly IHashTagRepository _hashTagRepositorySub;
        
        public TwitterControllerTests()
        {
            _tweetRepositorySub = Substitute.For<ITweetRepository>();
            _hashTagRepositorySub = Substitute.For<IHashTagRepository>();

            _controller = new TweetStatisticsController(NullLogger<TweetStatisticsController>.Instance, _tweetRepositorySub, _hashTagRepositorySub);
        }

        /// <summary>
        /// Tests the exposed endpoint of the TweetStatisticsController
        /// In a real-world scenario this would have a near 100% code coverage and test for all Status Codes
        /// and error cases
        /// </summary>
        [TestMethod]
        public void GetTweetStatistics_Succeeds_200Ok()
        {
            var tweetCount = 100;
            var topTenHashTags = new List<string>() { default, default, default, default, default, default, default, default, default, default };

            _tweetRepositorySub.GetCount().Returns(tweetCount);
            _hashTagRepositorySub.GetTopHashTags(Arg.Any<int>()).Returns(topTenHashTags);

            var result = _controller.Get().Result;
            var okObjResult =  result as OkObjectResult;
            var tweetStatistics = okObjResult?.Value as TweetStatistic;

            Assert.IsNotNull(result);
            Assert.IsTrue(okObjResult?.StatusCode == 200);
            Assert.AreEqual(tweetStatistics?.TotalTweets, 100);
            Assert.AreEqual(tweetStatistics?.TopTenHashTags, topTenHashTags);
        }
    }
}
