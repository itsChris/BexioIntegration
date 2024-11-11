using Newtonsoft.Json;

namespace BexioIntegration.Models.Response
{
    public class AccountResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("account_no")]
        public string AccountNo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account_type")]
        public int AccountType { get; set; }

        [JsonProperty("tax_id")]
        public int? TaxId { get; set; } // Nullable to handle null values in the JSON response

        [JsonProperty("fibu_account_group_id")]
        public int FibuAccountGroupId { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("is_locked")]
        public bool IsLocked { get; set; }
    }
}
