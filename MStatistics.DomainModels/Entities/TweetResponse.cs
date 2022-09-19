using Newtonsoft.Json;

namespace MStatistics.DomainModels.Entities
{
    public class TweetResponse
    {
        [JsonProperty("data")]
        public Tweet Tweet { get; set; }
    }
}