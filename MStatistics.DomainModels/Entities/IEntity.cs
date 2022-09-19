using Newtonsoft.Json;

namespace MStatistics.DomainModels.Entities
{
    public interface IEntity
    {
        [JsonProperty("id")]
        string Id { get; set; }
    }
}
