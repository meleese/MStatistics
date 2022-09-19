using Newtonsoft.Json;

namespace MStatistics.DomainModels.Entities
{
    public class HashTag : Entity
    {
        [JsonProperty("tweets")]
        public IList<Tweet> Tweets { get; set; }
    }
}
