using System.Runtime.Serialization;
using Service.PersonalData.Grpc.Contracts;

namespace Service.PersonalData.Grpc.Models
{
    [DataContract]
    public class RegisterPersonalDataGrpcModel
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public string Email { get; set; }
        
        [DataMember(Order = 3)]
        public string CountryOfResidence { get; set; }
        
        [DataMember(Order = 4)]
        public string CountryOfRegistration { get; set; }
        
        [DataMember(Order = 5)]
        public string IpOfRegistration { get; set; }
         
        [DataMember(Order = 6)]
        public AuditLogGrpcContract AuditLog { get; set; }
    }
}