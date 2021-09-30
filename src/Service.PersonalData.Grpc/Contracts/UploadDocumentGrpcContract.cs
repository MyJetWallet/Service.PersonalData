using System.Runtime.Serialization;
using SimpleTrading.PersonalData.Abstractions.Documents;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class UploadDocumentGrpcContract
    {
        [DataMember(Order = 1)]
        public string TraderId { get; set; }

        [DataMember(Order = 2)]
        public string FileName { get; set; }

        [DataMember(Order = 3)]
        public DocumentTypes DocumentType { get; set; }

        [DataMember(Order = 4)]
        public byte[] Data { get; set; }
    }
}