using System;
using System.Runtime.Serialization;
using Service.PersonalData.Grpc.Contracts;

namespace Service.PersonalData.Grpc.Models
{
    [DataContract]
    public class ConfirmGrpcModel
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public DateTime Confirm { get; set; }
        
        [DataMember(Order = 3)]
        public AuditLogGrpcContract AuditLog { get; set; }
    }
}