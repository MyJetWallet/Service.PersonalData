using System.Security.Cryptography;
using System.Text;
using Service.PersonalData.Postgres.Models;
using SimpleTrading.Common.Helpers;
using Helpers = SimpleTrading.Common.Helpers;

namespace Service.PersonalData.Services
{
    public static class CoderEncoder
    {
        public static void Encode(this PersonalDataPostgresEntity entity, byte[] key)
        {
            entity.City = entity.City?.EncodeString(key);
            entity.Email = entity.Email?.EncodeString(key);
            entity.Phone = entity.Phone?.EncodeString(key);
            entity.FirstName = entity.FirstName?.EncodeString(key);
            entity.LastName = entity.LastName?.EncodeString(key);
            entity.PostalCode = entity.PostalCode?.EncodeString(key);
            entity.CountryOfCitizenship = entity.CountryOfCitizenship?.EncodeString(key);
            entity.CountryOfResidence = entity.CountryOfResidence?.EncodeString(key);
            entity.Address = entity.Address?.EncodeString(key);
        }

        public static void Decode(this PersonalDataPostgresEntity entity, byte[] key)
        {
            entity.City = entity.City?.DecodeString(key);
            entity.Email = entity.Email?.DecodeString(key);
            entity.Phone = entity.Phone?.DecodeString(key);
            entity.FirstName = entity.FirstName?.DecodeString(key);
            entity.LastName = entity.LastName?.DecodeString(key);
            entity.PostalCode = entity.PostalCode?.DecodeString(key);
            entity.CountryOfCitizenship = entity.CountryOfCitizenship?.DecodeString(key);
            entity.CountryOfResidence = entity.CountryOfResidence?.DecodeString(key);
            entity.Address = entity.Address?.DecodeString(key);
        }

        public static string EncodeString(this string str, byte[] key)
        {
            var data = Encoding.UTF8.GetBytes(str);

            var result = AesEncodeDecode.Encode(data, key);
            
            return HexConverterUtils.ToHexString(result);
        }

        public static string DecodeString(this string str, byte[] key)
        {
            var data = HexConverterUtils.HexStringToByteArray(str);
            
            return Encoding.UTF8.GetString(AesEncodeDecode.Decode(data, key));
        }
    }
}