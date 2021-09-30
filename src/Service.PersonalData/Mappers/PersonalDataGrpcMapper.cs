using Service.PersonalData.Grpc.Models;
using SimpleTrading.PersonalData.Abstractions.PersonalData;

namespace Service.PersonalData.Mappers
{
    public static class PersonalDataGrpcMapper
    {
        public static PersonalDataGrpcModel ToGrpcModel(this IPersonalData src)
        {
            if (src == null)
                return null;
            
            return new PersonalDataGrpcModel
            {
                Id = src.Id,
                Email = src.Email,
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                City = src.City,
                Phone = src.Phone,
                Sex = src.Sex,
                PostalCode = src.PostalCode,
                USCitizen = src.USCitizen,
                Address = src.Address,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                CountryOfRegistration = src.CountryOfRegistration,
                KYC = src.KYC,
                Confirm = src.Confirm,
                ConfirmPhone = src.ConfirmPhone,
                BrandId = src.BrandId,
                PlatformType = src.PlatformType,
                CreatedAt = src.CreatedAt
            };
        }
        
        public static IPersonalData ToDomainModel(this RegisterPersonalDataGrpcModel src, bool isInternal)
        {
            if (src == null)
                return null;

            return new Postgres.Models.PersonalData
            {
                Id = src.Id,
                Email = src.Email,
                IsInternal = isInternal,
                CountryOfResidence = src.CountryOfResidence,
                CountryOfRegistration = src.CountryOfRegistration,
                IpOfRegistration = src.IpOfRegistration,
                USCitizen = null,
                KYC = PersonalDataKYCEnum.NotVerified
            };
        }
        
        public static IPersonalData ToDomainModel(this PersonalDataGrpcModel src)
        {
            if (src == null)
                return null;

            return new Postgres.Models.PersonalData
            {
                Id = src.Id,
                Email = src.Email,
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                City = src.City,
                Phone = src.Phone,
                Sex = src.Sex,
                PostalCode = src.PostalCode,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                KYC = src.KYC,
                Confirm = src.Confirm,
                ConfirmPhone = src.ConfirmPhone,
                USCitizen = src.USCitizen,
                Address = src.Address,
                BrandId = src.BrandId,
                PlatformType = src.PlatformType
            };
        }
    }
}