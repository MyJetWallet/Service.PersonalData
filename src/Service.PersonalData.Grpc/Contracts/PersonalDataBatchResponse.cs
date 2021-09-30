using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.PersonalData.Grpc.Models;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class PersonalDataBatchResponseContract
    {
        [DataMember(Order = 1)]
        public IEnumerable<PersonalDataGrpcModel> PersonalDatas { get; set; }
    }
}