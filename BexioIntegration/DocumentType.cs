using Newtonsoft.Json;

namespace BexioIntegration
{
    public enum DocumentType
    {
        [JsonProperty("kb_offer")]
        kb_offer,
        [JsonProperty("kb_order")]
        kb_order,
        [JsonProperty("kb_invoice")]
        kb_invoice
    }

}
