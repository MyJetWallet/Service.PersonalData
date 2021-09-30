using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Documents
{
    [DataContract]
    public class GetDocumentContentGrpcResponse
    {
        [DataMember(Order = 1)]
        public TraderDocumentContentGrpcModel DocumentContent { get; set; }
    }
    
    [DataContract]
    public class TraderDocumentContentGrpcModel
    {
        [DataMember(Order = 1)]
        public string Mime { get; set; }

        [DataMember(Order = 2)]
        public byte[] Data { get; set; }
        
        [DataMember(Order = 3)]
        public string FileName { get; set; }
    }
}