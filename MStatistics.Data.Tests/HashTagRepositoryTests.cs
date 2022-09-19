using Castle.Core.Logging;
using MStatistics.DomainModels;
using MStatistics.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using MStatistics.Data.Repositories;

namespace MStatistics.Data.Tests
{
    [TestClass]
    public class HashTagRepositoryTests
    {
        private readonly HashTagRepository _repository;


        public HashTagRepositoryTests()
        {
            _repositorySub = Substitute.For<IHashTagRepository>();

            _controller = new TweetStatisticsController(NullLogger<TweetStatisticsController>.Instance, _tweetRepositorySub, _hashTagRepositorySub);
        }

        [TestMethod]
        public void GetTweetStatistics_Succeeds_200Ok()
        {
            var tweetCount = 100;
            var topTenHashTags = new List<string>() { default, default, default, default, default, default, default, default, default, default };

            _tweetRepositorySub.GetCount().Returns(tweetCount);
            _hashTagRepositorySub.GetTopHashTags(Arg.Any<int>()).Returns(topTenHashTags);

            var result = _controller.Get().Result;
            var okObjResult = result as OkObjectResult;
            var tweetStatistics = okObjResult?.Value as TweetStatistic;

            Assert.IsNotNull(result);
            Assert.IsTrue(okObjResult?.StatusCode == 200);
            Assert.AreEqual(tweetStatistics?.TotalTweets, 100);
            Assert.AreEqual(tweetStatistics?.TopTenHashTags, topTenHashTags);
        }
    }
}
