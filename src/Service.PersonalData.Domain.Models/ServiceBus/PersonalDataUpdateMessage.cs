using System.Runtime.Serialization;
using SimpleTrading.PersonalData.Abstractions.PersonalDataUpdate;

namespace Service.PersonalData.Domain.Models.ServiceBus
{
    [DataContract]
    public class PersonalDataUpdateMessage : ITraderUpdate
    {
        public const string TopicName = "jet-wallet-personal-data-update";
        
        [DataMember(Order = 1)] public string TraderId { get; set; }
        
        
        public static PersonalDataUpdateMessage Create(ITraderUpdate src) =>
            new PersonalDataUpdateMessage()
            {
                TraderId = src.TraderId
            };
    }
}