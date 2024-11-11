using Newtonsoft.Json;

namespace BexioIntegration
{
    public class TaxDetail
    {
        [JsonProperty("percentage")]
        public string Percentage { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
