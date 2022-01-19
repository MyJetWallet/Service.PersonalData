using System;
using Service.PersonalData.Domain.Models;

namespace Service.PersonalData.Postgres.Models
{
    public class PersonalDataPostgresUpdateEntity : IPersonalDataUpdate
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string City { get; set; }
        
        public string Phone { get; set; }
        
        public string PostalCode { get; set; }
        
        public string CountryOfCitizenship { get; set; }
        
        public string CountryOfResidence { get; set; }
        
        public string Address { get; set; }
        
        public bool? UsCitizen { get; set; }
        
        public PersonalDataSexEnum? Sex { get; set; }
        
        public DateTime? BirthDay { get; set; }
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneIso { get; set; }

        public static PersonalDataPostgresUpdateEntity Create(IPersonalDataUpdate src)
        {
            return new PersonalDataPostgresUpdateEntity
            {
                Id = src.Id,
                FirstName = src.FirstName,
                LastName = src.LastName,
                City = src.City,
                Phone = src.Phone,
                PostalCode = src.PostalCode,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                Address = src.Address,
                UsCitizen = src.UsCitizen,
                Sex = src.Sex,
                BirthDay = src.BirthDay,
                PhoneCode = src.PhoneCode,
                PhoneNumber = src.PhoneNumber,
                PhoneIso = src.PhoneIso
            };
        }
    }
}