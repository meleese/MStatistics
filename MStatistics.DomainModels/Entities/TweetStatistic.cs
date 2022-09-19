namespace MStatistics.DomainModels.Entities
{
    public class TweetStatistic
    {
        /// <summary>
        /// Total number of tweets
        /// </summary>
        public int TotalTweets { get; set; }
        /// <summary>
        /// Total number of Hash Tags
        /// </summary>
        public int TotalHashTags { get; set; }
        /// <summary>
        /// Top ten hash tags at the moment
        /// </summary>
        public IEnumerable<string> TopTenHashTags { get; set; }
    }
}
