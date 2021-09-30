using System.Runtime.Serialization;
using SimpleTrading.PersonalData.Abstractions.PersonalData;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class UpdateKycGrpcContract
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public PersonalDataKYCEnum Kyc { get; set; }
        
        [DataMember(Order = 3)]
        public AuditLogGrpcContract AuditLog { get; set; }
    }
}