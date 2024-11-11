using Newtonsoft.Json;

namespace BexioIntegration.Models.Response
{
    public class TaxResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("digit")]
        public string Digit { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("tax_settlement_type")]
        public string TaxSettlementType { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("net_tax_value")]
        public double? NetTaxValue { get; set; } // Nullable to handle null values

        [JsonProperty("start_year")]
        public int? StartYear { get; set; } // Nullable to handle null values

        [JsonProperty("end_year")]
        public int? EndYear { get; set; } // Nullable to handle null values

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("start_month")]
        public int? StartMonth { get; set; } // Changed to nullable int

        [JsonProperty("end_month")]
        public int? EndMonth { get; set; } // Changed to nullable int
    }
}
