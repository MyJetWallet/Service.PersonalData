using System.Security.Cryptography;
using Service.PersonalData.Domain.Models;
using Service.PersonalData.Postgres.Models;
using Helpers = SimpleTrading.Common.Helpers;

namespace Service.PersonalData.Services
{
    public static class CoderEncoder
    {
        public static void Encode(this PersonalDataPostgresEntity entity, byte[] key)
        {
            entity.City = entity.City?.EncryptString(key);
            entity.Email = entity.Email?.EncryptString(key);
            entity.Phone = entity.Phone?.EncryptString(key);
            entity.PhoneCode = entity.PhoneCode?.EncryptString(key);
            entity.PhoneNumber = entity.PhoneNumber?.EncryptString(key);
            entity.FirstName = entity.FirstName?.EncryptString(key);
            entity.LastName = entity.LastName?.EncryptString(key);
            entity.PostalCode = entity.PostalCode?.EncryptString(key);
            entity.CountryOfCitizenship = entity.CountryOfCitizenship?.EncryptString(key);
            entity.CountryOfResidence = entity.CountryOfResidence?.EncryptString(key);
            entity.Address = entity.Address?.EncryptString(key);
        }

        public static void Decode(this PersonalDataPostgresEntity entity, byte[] key)
        {
            entity.City = entity.City?.DecryptString(key);
            entity.Email = entity.Email?.DecryptString(key);
            entity.Phone = entity.Phone?.DecryptString(key);
            entity.PhoneCode = entity.PhoneCode?.DecryptString(key);
            entity.PhoneNumber = entity.PhoneNumber?.DecryptString(key);
            entity.FirstName = entity.FirstName?.DecryptString(key);
            entity.LastName = entity.LastName?.DecryptString(key);
            entity.PostalCode = entity.PostalCode?.DecryptString(key);
            entity.CountryOfCitizenship = entity.CountryOfCitizenship?.DecryptString(key);
            entity.CountryOfResidence = entity.CountryOfResidence?.DecryptString(key);
            entity.Address = entity.Address?.DecryptString(key);
        }
    }
}