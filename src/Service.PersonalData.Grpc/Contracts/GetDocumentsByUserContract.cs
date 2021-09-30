using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetDocumentsByUserContract
    {
        [DataMember(Order = 1)]
        public string TraderId { get; set; }
    }

    [DataContract]
    public class GetDocumentsByUserResponse
    {
        [DataMember(Order = 1)]
        public TraderDocumentGrpcModel[] Documents { get; set; }
    }
}