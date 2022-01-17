using System;
using Service.PersonalData.Domain.Models;
using Service.PersonalData.Grpc.Models;

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
                CreatedAt = src.CreatedAt,
                IsTechnical = src.IsTechnical,
                PhoneCode = src.PhoneCode,
                PhoneNumber = src.PhoneNumber
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
                CountryOfRegistration = src.CountryOfRegistration,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                KYC = src.KYC,
                Confirm = src.Confirm,
                ConfirmPhone = src.ConfirmPhone,
                USCitizen = src.USCitizen,
                IpOfRegistration = src.IpOfRegistration,
                Address = src.Address,
                BrandId = src.BrandId,
                PlatformType = src.PlatformType,
                IsTechnical = src.IsTechnical,
                CreatedAt = DateTime.UtcNow,
                PhoneCode = src.PhoneCode,
                PhoneNumber = src.PhoneNumber
            };
        }
    }
}