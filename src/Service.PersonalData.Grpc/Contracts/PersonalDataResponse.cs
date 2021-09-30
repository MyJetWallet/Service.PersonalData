using System.Runtime.Serialization;
using Service.PersonalData.Grpc.Models;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class PersonalDataGrpcResponseContract
    {
        [DataMember(Order = 1)]
        public PersonalDataGrpcModel PersonalData { get; set; }
    }
}