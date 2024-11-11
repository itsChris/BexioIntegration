using Newtonsoft.Json;

namespace BexioIntegration.Models.Response
{
    public class UnitResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}
