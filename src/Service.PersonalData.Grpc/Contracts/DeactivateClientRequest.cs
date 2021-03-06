using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts;

[DataContract]
public class DeactivateClientRequest
{
    [DataMember(Order = 1)] public string Id { get; set; }
    
    [DataMember(Order = 2)]
    public AuditLogGrpcContract AuditLog { get; set; }
}