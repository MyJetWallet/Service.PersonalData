using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class AuditLogGrpcContract
    {
        [DataMember(Order = 1)]
        public string TraderId { get; set; }

        [DataMember(Order = 2)]
        public string Ip { get; set; }
        
        [DataMember(Order = 3)]
        public string ServiceName { get; set; }

        [DataMember(Order = 4)]
        public string ProcessId { get; set; }
        
        [DataMember(Order = 5)]
        public string Context { get; set; }
    }
}