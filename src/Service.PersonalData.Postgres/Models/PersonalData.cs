using System;
using Service.PersonalData.Domain.Models;

namespace Service.PersonalData.Postgres.Models
{
    public class PersonalData : IPersonalData
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string City { get; set; }
        
        public string Phone { get; set; }
        
        public PersonalDataSexEnum? Sex { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string PostalCode { get; set; }
        
        public string CountryOfCitizenship { get; set; }
        
        public string CountryOfResidence { get; set; }
        
        public PersonalDataKYCEnum? KYC { get; set; }
        
        public DateTime? Confirm { get; set; }
        
        public DateTime? ConfirmPhone { get; set; }
        
        public string? Address { get; set; }
        
        public bool? USCitizen { get; set; }
        
        public string IpOfRegistration { get; set; }
        
        public string CountryOfRegistration { get; set; }
        
        public bool IsInternal { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string EmailHash { get; set; }
        
        public string EmailGroupId { get; set; }
        
        public string BrandId { get; set; }
        
        public string PlatformType { get; set; }
        
        public bool IsTechnical { get; set; }

    }
}