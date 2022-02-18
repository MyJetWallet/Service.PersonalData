using System;
using System.Runtime.Serialization;
using Service.PersonalData.Domain.Models;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class UpdatePersonalDataGrpcContract
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public string FirstName { get; set; }
        
        [DataMember(Order = 3)]
        public string LastName { get; set; }
        
        [DataMember(Order = 4)]
        public PersonalDataSexEnum? Sex { get; set; }
        
        [DataMember(Order = 5)]
        public DateTime? DateOfBirth { get; set; }
        
        [DataMember(Order = 6)]
        public string CountryOfResidence { get; set; }
        
        [DataMember(Order = 7)]
        public string CountryOfCitizenship { get; set; }
        
        [DataMember(Order = 8)]
        public string City { get; set; }
        
        [DataMember(Order = 9)]
        public string PostalCode { get; set; }
        
        [DataMember(Order = 10)]
        public string Phone { get; set; }
        
        [DataMember(Order = 11)]
        public AuditLogGrpcContract AuditLog { get; set; }

        [DataMember(Order = 12)]
        public string Address { get; set; }

        [DataMember(Order = 13)]
        public bool? USCitizen { get; set; }
        
        [DataMember(Order = 14)]
        public string PhoneCode { get; set; }
        
        [DataMember(Order = 15)]
        public string PhoneNumber { get; set; }
        
        [DataMember(Order = 16)]
        public string PhoneIso { get; set; }
        [DataMember(Order = 17)]
        public string PhoneNational { get; set; }

    }
}