using Newtonsoft.Json;

namespace MStatistics.DomainModels.Entities
{
    public class Tweet : Entity
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("hashTags")]
        public IList<HashTag> hashTags { get; set; }
    }
}
