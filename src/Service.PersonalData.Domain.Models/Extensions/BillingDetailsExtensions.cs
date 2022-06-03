namespace Service.PersonalData.Domain.Models.NoSql
{
    public static class BillingDetailsExtensions
    {
        public static BillingDetails GetUnEncrypted(this BillingDetails billingDetails, byte[] secret)
        {
            return new BillingDetails()
            {
                ClientId = billingDetails.ClientId,
                BillingCity = billingDetails.BillingCity?.DecryptString(secret),
                BillingCountry = billingDetails.BillingCountry?.DecryptString(secret),
                BillingDistrict = billingDetails.BillingDistrict?.DecryptString(secret),
                BillingLine1 = billingDetails.BillingLine1?.DecryptString(secret),
                BillingLine2 = billingDetails.BillingLine2?.DecryptString(secret),
                BillingName = billingDetails.BillingName?.DecryptString(secret),
                BillingPostalCode = billingDetails.BillingPostalCode?.DecryptString(secret),
            };
        }

        public static BillingDetails GetEncrypted(this BillingDetails billingDetails, byte[] secret)
        {
            return new BillingDetails()
            {
                ClientId = billingDetails.ClientId,
                BillingCity = billingDetails.BillingCity?.EncryptString(secret),
                BillingCountry = billingDetails.BillingCountry?.EncryptString(secret),
                BillingDistrict = billingDetails.BillingDistrict?.EncryptString(secret),
                BillingLine1 = billingDetails.BillingLine1?.EncryptString(secret),
                BillingLine2 = billingDetails.BillingLine2?.EncryptString(secret),
                BillingName = billingDetails.BillingName?.EncryptString(secret),
                BillingPostalCode = billingDetails.BillingPostalCode?.EncryptString(secret),
            };
        }
    }
}
