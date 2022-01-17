using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.PersonalData.Domain.Models;
using Service.PersonalData.Grpc.Contracts;

namespace Service.PersonalData.Grpc.Models
{
    [DataContract]
    public class PersonalDataGrpcModel
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public string Email { get; set; }

        [DataMember(Order = 3)]
        public string FirstName { get; set; }
        
        [DataMember(Order = 4)]
        public string LastName { get; set; }
        
        [DataMember(Order = 5)]
        public PersonalDataSexEnum? Sex { get; set; }
        
        [DataMember(Order = 6)]
        public DateTime? DateOfBirth { get; set; }
        
        [DataMember(Order = 7)]
        public string CountryOfResidence { get; set; }
        
        [DataMember(Order = 8)]
        public string CountryOfCitizenship { get; set; }
        
        [DataMember(Order = 9)]
        public string City { get; set; }
        
        [DataMember(Order = 10)]
        public string PostalCode { get; set; }
        
        [DataMember(Order = 11)]
        public string Phone { get; set; }
        
        [DataMember(Order = 12)]
        public PersonalDataKYCEnum? KYC { get; set; }
        
        [DataMember(Order = 13)]
        public DateTime? Confirm { get; set; }
        
        [DataMember(Order = 14)]
        public AuditLogGrpcContract AuditLog { get; set; }
        
        [DataMember(Order = 15)]
        public string Address { get; set; }

        [DataMember(Order = 16)]
        public bool? USCitizen { get; set; }
        
        [DataMember(Order = 17)]
        public string? CountryOfRegistration { get; set; }
        
        [DataMember(Order = 18)]
        public string? IpOfRegistration { get; set; }

        [DataMember(Order = 19)]
        public IEnumerable<ExternalDataGrpcModel> ExternalData { get; set; }

        [DataMember(Order = 20)]
        public string BrandId { get; set; }

        [DataMember(Order = 21)]
        public string PlatformType { get; set; }
        
        [DataMember(Order = 22)]
        public DateTime? ConfirmPhone { get; set; }

        [DataMember(Order = 23)]
        public DateTime CreatedAt { get; set; }
        
        [DataMember(Order = 24)]
        public bool IsTechnical { get; set; }
        
        [DataMember(Order = 25)]
        public string PhoneCode { get; set; }
        
        [DataMember(Order = 26)]
        public string PhoneNumber { get; set; }

    }
}