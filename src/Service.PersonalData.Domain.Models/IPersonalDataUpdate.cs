using System;

namespace Service.PersonalData.Domain.Models
{
    public interface IPersonalDataUpdate
    {
        string Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string City { get; set; }

        string Phone { get; set; }

        string PostalCode { get; set; }

        string CountryOfCitizenship { get; set; }

        string CountryOfResidence { get; set; }

        string Address { get; set; }

        bool? UsCitizen { get; set; }

        PersonalDataSexEnum? Sex { get; set; }

        DateTime? BirthDay { get; set; }
        
        string PhoneCode { get; set; }
        
        string PhoneNumber { get; set; }
        string PhoneIso { get; set; }
    }
}