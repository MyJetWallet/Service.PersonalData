using Service.PersonalData.Domain.Models;
using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class SetBillingDetailsGrpcModel
    {
        [DataMember(Order = 1)]
        public BillingDetails BillingDetails
        {
            get;
            set;
        }
    }
}