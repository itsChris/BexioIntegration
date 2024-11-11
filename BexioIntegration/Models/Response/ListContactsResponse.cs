using Newtonsoft.Json;

namespace BexioIntegration.Models.Response
{
    public class ListContactsResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nr")]
        public string Nr { get; set; }

        [JsonProperty("contact_type_id")]
        public int ContactTypeId { get; set; }

        [JsonProperty("name_1")]
        public string Name1 { get; set; }

        [JsonProperty("name_2")]
        public string Name2 { get; set; }

        [JsonProperty("salutation_id")]
        public int? SalutationId { get; set; }

        [JsonProperty("salutation_form")]
        public string SalutationForm { get; set; }

        [JsonProperty("title_id")]
        public int? TitleId { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country_id")]
        public int CountryId { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("mail_second")]
        public string MailSecond { get; set; }

        [JsonProperty("phone_fixed")]
        public string PhoneFixed { get; set; }

        [JsonProperty("phone_fixed_second")]
        public string PhoneFixedSecond { get; set; }

        [JsonProperty("phone_mobile")]
        public string PhoneMobile { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("skype_name")]
        public string SkypeName { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("language_id")]
        public int? LanguageId { get; set; }

        [JsonProperty("is_lead")]
        public bool IsLead { get; set; }

        [JsonProperty("contact_group_ids")]
        public string ContactGroupIds { get; set; }

        [JsonProperty("contact_branch_ids")]
        public string ContactBranchIds { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
