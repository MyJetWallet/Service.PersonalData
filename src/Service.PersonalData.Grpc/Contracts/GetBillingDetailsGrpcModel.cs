using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetBillingDetailsGrpcModel
    {
        [DataMember(Order = 1)]
        public string ClientId
        {
            get;
            set;
        }

        [DataMember(Order = 2)]
        public string Uid
        {
            get;
            set;
        }

        [DataMember(Order = 3)]
        public string Secret
        {
            get;
            set;
        }
    }
}