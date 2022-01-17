using System;
namespace Service.PersonalData.Domain.Models
{
    public interface IPersonalData
    {
        string Id { get; }

        string Email { get; }

        string FirstName { get; }

        string LastName { get; }

        string City { get; }

        string Phone { get; }

        PersonalDataSexEnum? Sex { get; }

        DateTime? DateOfBirth { get; }

        string PostalCode { get; }

        string CountryOfCitizenship { get; }

        string CountryOfResidence { get; }

        PersonalDataKYCEnum? KYC { get; }

        DateTime? Confirm { get; }

        DateTime? ConfirmPhone { get; }

        string? Address { get; }

        bool? USCitizen { get; }

        string IpOfRegistration { get; }

        string CountryOfRegistration { get; }

        bool IsInternal { get; }

        DateTime CreatedAt { get; }

        string EmailHash { get; }

        string EmailGroupId { get; }

        string BrandId { get; }

        string PlatformType { get; }
        
        bool IsTechnical { get; set; }
        string PhoneCode { get; set; }
        string PhoneNumber { get; set; }
    }
}