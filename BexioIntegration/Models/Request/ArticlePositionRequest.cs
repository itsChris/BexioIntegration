using Newtonsoft.Json;

namespace BexioIntegration.Models.Request
{
    public class ArticlePositionRequest
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("unit_id")]
        public int UnitId { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("tax_id")]
        public int TaxId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("unit_price")]
        public string UnitPrice { get; set; }

        [JsonProperty("discount_in_percent")]
        public string DiscountInPercent { get; set; }

        [JsonProperty("article_id")]
        public int ArticleId { get; set; }
    }

}
