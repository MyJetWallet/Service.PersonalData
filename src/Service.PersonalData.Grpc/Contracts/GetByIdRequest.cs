using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetByIdRequest
    {
        [DataMember(Order = 1)] public string Id { get; set; }
    }
}