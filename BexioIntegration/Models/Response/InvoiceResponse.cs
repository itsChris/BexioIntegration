using Newtonsoft.Json;
using System.Collections.Generic;

namespace BexioIntegration.Models.Response
{
    public class InvoiceResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("document_nr")]
        public string DocumentNr { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("contact_id")]
        public int ContactId { get; set; }

        [JsonProperty("contact_sub_id")]
        public int? ContactSubId { get; set; } // Nullable for potential null values

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("project_id")]
        public int? ProjectId { get; set; } // Nullable for potential null values

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

        [JsonProperty("total_gross")]
        public string TotalGross { get; set; }

        [JsonProperty("total_net")]
        public string TotalNet { get; set; }

        [JsonProperty("total_taxes")]
        public string TotalTaxes { get; set; }

        [JsonProperty("total_received_payments")]
        public string TotalReceivedPayments { get; set; }

        [JsonProperty("total_credit_vouchers")]
        public string TotalCreditVouchers { get; set; }

        [JsonProperty("total_remaining_payments")]
        public string TotalRemainingPayments { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("total_rounding_difference")]
        public double TotalRoundingDifference { get; set; }

        [JsonProperty("mwst_type")]
        public int MwstType { get; set; }

        [JsonProperty("mwst_is_net")]
        public bool MwstIsNet { get; set; }

        [JsonProperty("show_position_taxes")]
        public bool ShowPositionTaxes { get; set; }

        [JsonProperty("is_valid_from")]
        public string IsValidFrom { get; set; }

        [JsonProperty("is_valid_to")]
        public string IsValidTo { get; set; }

        [JsonProperty("contact_address")]
        public string ContactAddress { get; set; }

        [JsonProperty("kb_item_status_id")]
        public int KbItemStatusId { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("api_reference")]
        public string ApiReference { get; set; }

        [JsonProperty("viewed_by_client_at")]
        public string ViewedByClientAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("esr_id")]
        public int EsrId { get; set; }

        [JsonProperty("qr_invoice_id")]
        public int QrInvoiceId { get; set; }

        [JsonProperty("template_slug")]
        public string TemplateSlug { get; set; }

        [JsonProperty("taxs")]
        public List<TaxDetail> Taxes { get; set; }

        [JsonProperty("network_link")]
        public string NetworkLink { get; set; }
    }


}
