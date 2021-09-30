using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetTotalResponse
    {
        [DataMember(Order = 1)]
        public int TotalPersonalDatas { get; set; }
    }
}
