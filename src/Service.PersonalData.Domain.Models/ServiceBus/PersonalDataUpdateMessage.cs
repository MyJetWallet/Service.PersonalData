using System.Runtime.Serialization;

namespace Service.PersonalData.Domain.Models.ServiceBus
{
    [DataContract]
    public class PersonalDataUpdateMessage 
    {
        public const string TopicName = "jet-wallet-personal-data-update";
        
        [DataMember(Order = 1)] public string TraderId { get; set; }
        
        
        public static PersonalDataUpdateMessage Create(string traderId) =>
            new PersonalDataUpdateMessage()
            {
                TraderId = traderId
            };
    }
}