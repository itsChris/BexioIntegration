using Newtonsoft.Json;

namespace BexioIntegration.Models.Response
{
    public class ArticleResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("article_type_id")]
        public int ArticleTypeId { get; set; }

        [JsonProperty("contact_id")]
        public int? ContactId { get; set; } // Nullable to handle null values

        [JsonProperty("deliverer_code")]
        public string DelivererCode { get; set; }

        [JsonProperty("deliverer_name")]
        public string DelivererName { get; set; }

        [JsonProperty("deliverer_description")]
        public string DelivererDescription { get; set; }

        [JsonProperty("intern_code")]
        public string InternCode { get; set; }

        [JsonProperty("intern_name")]
        public string InternName { get; set; }

        [JsonProperty("intern_description")]
        public string InternDescription { get; set; }

        [JsonProperty("purchase_price")]
        public double? PurchasePrice { get; set; } // Nullable to handle null values

        [JsonProperty("sale_price")]
        public double? SalePrice { get; set; } // Nullable to handle null values

        [JsonProperty("purchase_total")]
        public double? PurchaseTotal { get; set; } // Nullable to handle null values

        [JsonProperty("sale_total")]
        public double? SaleTotal { get; set; } // Nullable to handle null values

        [JsonProperty("currency_id")]
        public int? CurrencyId { get; set; } // Nullable to handle null values

        [JsonProperty("tax_income_id")]
        public int? TaxIncomeId { get; set; } // Nullable to handle null values

        [JsonProperty("tax_id")]
        public int? TaxId { get; set; } // Nullable to handle null values

        [JsonProperty("tax_expense_id")]
        public int? TaxExpenseId { get; set; } // Nullable to handle null values

        [JsonProperty("unit_id")]
        public int? UnitId { get; set; } // Nullable to handle null values

        [JsonProperty("is_stock")]
        public bool IsStock { get; set; }

        [JsonProperty("stock_id")]
        public int? StockId { get; set; } // Nullable to handle null values

        [JsonProperty("stock_place_id")]
        public int? StockPlaceId { get; set; } // Nullable to handle null values

        [JsonProperty("stock_nr")]
        public int StockNr { get; set; }

        [JsonProperty("stock_min_nr")]
        public int StockMinNr { get; set; }

        [JsonProperty("stock_reserved_nr")]
        public int StockReservedNr { get; set; }

        [JsonProperty("stock_available_nr")]
        public int StockAvailableNr { get; set; }

        [JsonProperty("stock_picked_nr")]
        public int StockPickedNr { get; set; }

        [JsonProperty("stock_disposed_nr")]
        public int StockDisposedNr { get; set; }

        [JsonProperty("stock_ordered_nr")]
        public int StockOrderedNr { get; set; }

        [JsonProperty("width")]
        public double? Width { get; set; } // Nullable to handle null values

        [JsonProperty("height")]
        public double? Height { get; set; } // Nullable to handle null values

        [JsonProperty("weight")]
        public double? Weight { get; set; } // Nullable to handle null values

        [JsonProperty("volume")]
        public double? Volume { get; set; } // Nullable to handle null values

        [JsonProperty("html_text")]
        public string HtmlText { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("delivery_price")]
        public double? DeliveryPrice { get; set; } // Nullable to handle null values

        [JsonProperty("article_group_id")]
        public int? ArticleGroupId { get; set; } // Nullable to handle null values
    }
}
