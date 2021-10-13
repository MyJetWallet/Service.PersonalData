using System;
using Newtonsoft.Json;
using Service.PersonalData.Domain.Models;

namespace Service.PersonalData.Postgres.Models
{
    public class PersonalDataPostgresEntity : IPersonalData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("dateofbirth")]
        public DateTime? DateOfBirth { get; set; }
        
        [JsonProperty("postalcode")]
        public string PostalCode { get; set; }
        
        [JsonProperty("countryofcitizenship")]
        public string CountryOfCitizenship { get; set; }
        
        [JsonProperty("countryofresidence")]
        public string CountryOfResidence { get; set; }
        
        [JsonProperty("emailhash")]
        public string EmailHash { get; set; }
        
        [JsonProperty("sex")]
        public PersonalDataSexEnum? Sex { get; set; }
        
        [JsonProperty("kyc")]
        public PersonalDataKYCEnum? KYC { get; set; }
        
        [JsonProperty("confirm")]
        public DateTime? Confirm { get; set; }
        
        [JsonProperty("confirmphone")]
        public DateTime? ConfirmPhone { get; set; }
        
        [JsonProperty("address")]
        public string? Address { get; set; }
        
        [JsonProperty("uscitizen")]
        public bool? USCitizen { get; set; }
        
        [JsonProperty("ipofregistration")]
        public string IpOfRegistration { get; set; }
        
        [JsonProperty("countryofregistration")]
        public string CountryOfRegistration { get; set; }
        
        [JsonProperty("isinternal")]
        public bool IsInternal { get; set; }

        [JsonProperty("createdat")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("emailgroupid")]
        public string EmailGroupId { get; set; }

        [JsonProperty("brandid")]
        public string BrandId { get; set; }
        
        [JsonProperty("platformtype")]
        public string PlatformType { get; set; }

        [JsonProperty("istechnical")]

        public bool IsTechnical { get; set; }

        public static PersonalDataPostgresEntity Create(IPersonalData src)
        {
            return new PersonalDataPostgresEntity
            {
                Id = src.Id,
                Email = src.Email,
                City = src.City,
                Phone = src.Phone,
                Sex = src.Sex,
                FirstName = src.FirstName,
                LastName = src.LastName,
                PostalCode = src.PostalCode,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                DateOfBirth = src.DateOfBirth,
                KYC = src.KYC,
                Confirm = src.Confirm,
                ConfirmPhone = src.ConfirmPhone,
                Address = src.Address,
                USCitizen = src.USCitizen,
                IpOfRegistration = src.IpOfRegistration,
                CountryOfRegistration = src.CountryOfRegistration,
                CreatedAt = src.CreatedAt,
                EmailHash = src.EmailHash,
                IsInternal = src.IsInternal,
                EmailGroupId = src.EmailGroupId,
                BrandId = src.BrandId,
                PlatformType = src.PlatformType
            };
        }

        public Service.PersonalData.Postgres.Models.PersonalData ToPersonalData()
        {
            return new PersonalData
            {
                Id = Id,
                Email = Email,
                City = City,
                Phone = Phone,
                Sex = Sex,
                FirstName = FirstName,
                LastName = LastName,
                PostalCode = PostalCode,
                CountryOfCitizenship = CountryOfCitizenship,
                CountryOfResidence = CountryOfResidence,
                DateOfBirth = DateOfBirth,
                KYC = KYC,
                Confirm = Confirm,
                ConfirmPhone = ConfirmPhone,
                Address = Address,
                USCitizen = USCitizen,
                IpOfRegistration = IpOfRegistration,
                CountryOfRegistration = CountryOfRegistration,
                CreatedAt = CreatedAt,
                EmailHash = EmailHash,
                IsInternal = IsInternal,
                EmailGroupId = EmailGroupId,
                PlatformType = PlatformType,
                BrandId = BrandId
            };
        }
    }
}