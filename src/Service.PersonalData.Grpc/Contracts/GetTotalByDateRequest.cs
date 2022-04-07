using System;
using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts;

[DataContract]
public class GetTotalByDateRequest
{
    [DataMember(Order = 1)]
    public DateTime From { get; set; }

    [DataMember(Order = 2)]
    public DateTime To { get; set; }
}