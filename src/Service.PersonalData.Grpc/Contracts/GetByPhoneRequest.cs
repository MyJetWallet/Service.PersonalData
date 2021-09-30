using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetByPhoneRequest
    {
        [DataMember(Order = 1)] public string Phone { get; set; }
    }
}