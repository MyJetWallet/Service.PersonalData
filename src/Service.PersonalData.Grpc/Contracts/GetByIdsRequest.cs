using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetByIdsRequest
    {
        [DataMember(Order = 1)] public List<string> Ids { get; set; }
    }
}