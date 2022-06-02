using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.PersonalData.Domain.Models
{
    [DataContract]
    public class BillingDetails
    {
        [DataMember(Order = 1)]
        public string ClientId
        {
            get;
            set;
        }

        [DataMember(Order = 2)]
        public string BillingName
        {
            get;
            set;
        }

        [DataMember(Order = 3)]
        public string BillingCity
        {
            get;
            set;
        }

        [DataMember(Order = 4)]
        public string BillingCountry
        {
            get;
            set;
        }

        [DataMember(Order = 5)]
        public string BillingLine1
        {
            get;
            set;
        }

        [DataMember(Order = 6)]
        public string BillingLine2
        {
            get;
            set;
        }

        [DataMember(Order = 7)]
        public string BillingDistrict
        {
            get;
            set;
        }

        [DataMember(Order = 8)]
        public string BillingPostalCode
        {
            get;
            set;
        }

    }
}
