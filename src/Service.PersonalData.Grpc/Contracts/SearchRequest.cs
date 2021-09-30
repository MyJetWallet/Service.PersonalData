using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class SearchRequest
    {
        [DataMember(Order = 1)] public string SearchText { get; set; }
    }
}