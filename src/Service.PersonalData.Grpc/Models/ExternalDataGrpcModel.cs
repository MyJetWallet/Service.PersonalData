using System.Runtime.Serialization;

namespace Service.PersonalData.Grpc.Models
{
    [DataContract]
    public class ExternalDataGrpcModel
    {
        [DataMember(Order = 1)]
        public string Key { get; set; }

        [DataMember(Order = 2)]
        public string Value { get; set; }

        public static ExternalDataGrpcModel Create(string key, string value)
        {
            return new ExternalDataGrpcModel
            {
                Key = key,
                Value = value
            };
        }
    }
}