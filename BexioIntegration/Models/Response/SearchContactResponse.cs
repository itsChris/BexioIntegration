namespace BexioIntegration.Models.Response
{
    using System;

    namespace BexioIntegration.Models.Response
    {
        public class SearchContactResponse
        {
            public int Id { get; set; }
            public string Nr { get; set; }
            public int ContactTypeId { get; set; }
            public string Name1 { get; set; }
            public string Name2 { get; set; }
            public int? SalutationId { get; set; }
            public string SalutationForm { get; set; }
            public int? TitleId { get; set; }
            public DateTime? Birthday { get; set; }
            public string Address { get; set; }
            public string Postcode { get; set; }
            public string City { get; set; }
            public int CountryId { get; set; }
            public string Mail { get; set; }
            public string MailSecond { get; set; }
            public string PhoneFixed { get; set; }
            public string PhoneFixedSecond { get; set; }
            public string PhoneMobile { get; set; }
            public string Fax { get; set; }
            public string Url { get; set; }
            public string SkypeName { get; set; }
            public string Remarks { get; set; }
            public int? LanguageId { get; set; }
            public bool IsLead { get; set; }
            public string ContactGroupIds { get; set; }
            public string ContactBranchIds { get; set; }
            public int UserId { get; set; }
            public int OwnerId { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }

}
