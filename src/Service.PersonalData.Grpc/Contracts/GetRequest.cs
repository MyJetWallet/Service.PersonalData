using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetRequest
    {
        [DataMember(Order = 1)]
        public int Limit { get; set; }

        [DataMember(Order = 2)]
        public int Offset { get; set; }
    }
}
