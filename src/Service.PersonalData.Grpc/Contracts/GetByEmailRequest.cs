using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetByEmailRequest
    {
        [DataMember(Order = 1)] public string Email { get; set; }
    }
}