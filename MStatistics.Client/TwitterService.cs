using Microsoft.Extensions.Configuration;
using MStatistics.DomainModels.Entities;
using System.Net;
using System.Text;
using MStatistics.DomainModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace MStatistics.Client
{
    public class TwitterService : ITwitterService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ITwitterService> _logger;
        private readonly ITweetRepository _tweetRepository;
        private readonly IHashTagRepository _hashTagRepository;
        private readonly IConfiguration _configuration;

        public TwitterService(ILogger<ITwitterService> logger, HttpClient httpClient, ITweetRepository tweetRepository, IHashTagRepository hashTagRepository, IConfiguration configurtion)
        {
            _logger = logger;
            _httpClient = httpClient;
            _tweetRepository = tweetRepository;
            _hashTagRepository = hashTagRepository;
            _configuration = configurtion;
        }

        /// <inheritdoc />
        public async Task Invoke(CancellationToken cancellationToken)
        {
            WebRequest request = WebRequest.Create(_httpClient.BaseAddress);
            request.Headers.Add("Authorization", $"Bearer {_configuration["TwitterServiceBearerToken"]}");

            using (WebResponse response = request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                jsonTextReader.SupportMultipleContent = true;
                JsonSerializer serializer = new JsonSerializer();
                Regex hashTagFinder = new Regex("(^|\\B)#(?![0-9_]+\\b)([a-zA-Z0-9_]{1,30})(\\b|\\r)");

                while (await jsonTextReader.ReadAsync() && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var tweetResponse = serializer.Deserialize<TweetResponse>(jsonTextReader);

                        if (tweetResponse != null && tweetResponse.Tweet != null)
                        {
                            var tweet = new Tweet() { Id = Guid.NewGuid().ToString(), Text = tweetResponse.Tweet.Text };

                            var matches = hashTagFinder.Matches(tweet.Text);

                            if (matches == null || !matches.Any())
                            {
                                await _tweetRepository.Add(tweet);
                            }
                            else
                            {
                                foreach (Match match in matches)
                                {
                                    foreach (Capture capture in match.Captures)
                                    {
                                        var tag = capture.Value;
                                        var hashTag = await _hashTagRepository.GetById(tag);

                                        if (hashTag != null)
                                        {
                                            hashTag.Tweets.Add(tweet);
                                            await _hashTagRepository.Update(hashTag);

                                        }
                                        else
                                        {
                                            await _hashTagRepository.Add(new HashTag() { Id = tag, Tweets = new List<Tweet>() { tweet } });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"An error was encountered while executing {nameof(Invoke)} in {nameof(TwitterService)}. Message: {e.Message}. Inner Exception: {e.InnerException}. Stack Trace: {e.StackTrace}");
                    }
                }

                jsonTextReader.Close();
                streamReader.Close();
                stream.Close();
                response.Close();
            }
        }
    }
}