using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.PersonalData.Grpc.Models;
using SimpleTrading.PersonalData.Abstractions.PersonalData;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetPersonalDataByStatusRequest
    {
        [DataMember(Order = 1)]
        public PersonalDataKYCEnum Status { get; set; }
    }
    
    [DataContract]
    public class GetPersonalDataByStatusResponse
    {
        [DataMember(Order = 1)]
        public IEnumerable<PersonalDataGrpcModel> PersonalDataModels { get; set; }
    }
}