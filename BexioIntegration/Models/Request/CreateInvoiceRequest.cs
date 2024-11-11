using Newtonsoft.Json;

namespace BexioIntegration.Models.Request
{
    public class CreateInvoiceRequest
    {
        [JsonProperty("document_nr")]
        public string DocumentNr { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("contact_id")]
        public int ContactId { get; set; }

        [JsonProperty("contact_sub_id")]
        public int? ContactSubId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("pr_project_id")]
        public int? PrProjectId { get; set; }

        [JsonProperty("logopaper_id")]
        public int LogopaperId { get; set; }

        [JsonProperty("language_id")]
        public int LanguageId { get; set; }

        [JsonProperty("bank_account_id")]
        public int BankAccountId { get; set; }

        [JsonProperty("currency_id")]
        public int CurrencyId { get; set; }

        [JsonProperty("payment_type_id")]
        public int PaymentTypeId { get; set; }

        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("footer")]
        public string Footer { get; set; }

        [JsonProperty("mwst_type")]
        public int MwstType { get; set; }

        [JsonProperty("mwst_is_net")]
        public bool MwstIsNet { get; set; }

        [JsonProperty("show_position_taxes")]
        public bool ShowPositionTaxes { get; set; }

        [JsonProperty("is_valid_from")]
        public DateTime IsValidFrom { get; set; }

        [JsonProperty("is_valid_to")]
        public DateTime IsValidTo { get; set; }

        [JsonProperty("contact_address_manual")]
        public string ContactAddressManual { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("api_reference")]
        public string ApiReference { get; set; }

        [JsonProperty("template_slug")]
        public string TemplateSlug { get; set; }

        [JsonProperty("positions")]
        public List<ArticlePositionRequest> Positions { get; set; }
    }
}
