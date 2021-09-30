using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Models
{
    [DataContract]
    public class ResultGrpcResponse
    {
        [DataMember(Order = 1)]
        public bool Ok { get; set; }
    }
}