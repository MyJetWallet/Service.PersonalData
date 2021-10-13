using System;
using System.Runtime.Serialization;
using Service.PersonalData.Domain.Models;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class DocumentGrpcContract
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public string TraderId { get; set; }
    }

    [DataContract]
    public class TraderDocumentGrpcModel
    {
        [DataMember(Order = 1)]
        public string TraderId { get; set; }

        [DataMember(Order = 2)]
        public string Id { get; set; }

        [DataMember(Order = 3)]
        public DocumentTypes DocumentType { get; set; }

        [DataMember(Order = 4)]
        public DateTime DateTime { get; set; }

        [DataMember(Order = 5)]
        public string Mime { get; set; }
        
        [DataMember(Order = 6)]
        public string FileName { get; set; }
    }
}