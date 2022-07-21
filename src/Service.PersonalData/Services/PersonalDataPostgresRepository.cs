using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Service.PersonalData.Domain.Models;
using Service.PersonalData.Postgres;
using Service.PersonalData.Postgres.Models;

namespace Service.PersonalData.Services
{
    public class PersonalDataPostgresRepository
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;

        private readonly ILogger<PersonalDataPostgresRepository> _logger;

        private const string TableName = "personaldata";
        
        public PersonalDataPostgresRepository(ILogger<PersonalDataPostgresRepository> logger, DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
        {
            _logger = logger;
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        private async Task<PersonalDataPostgresEntity> GetEntityAsync(string id, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                
                var result = await ctx.PersonalDataSet.FirstOrDefaultAsync(t => t.Id == id);
                
                result?.Decode(initKey);

                return result;
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<PersonalDataPostgresEntity>> GetByIdsAsync(IEnumerable<string> ids, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                var results = ctx.PersonalDataSet.Where(t => ids.Contains(t.Id)).ToList();

                var decodedResults = new List<PersonalDataPostgresEntity>();

                foreach (var result in results)
                {
                    try
                    {
                        result?.Decode(initKey);
                        decodedResults.Add(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return decodedResults;
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<PersonalDataPostgresEntity>> GetAsync(int limit, int offset, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                var results = ctx.PersonalDataSet.OrderBy(t => t.Id)
                    .Skip(offset).Take(limit).ToList();
                
                var decodedResults = new List<PersonalDataPostgresEntity>();

                foreach (var result in results)
                {
                    try
                    {
                        result?.Decode(initKey);
                        decodedResults.Add(result);
                    }
                    catch { }
                }

                return decodedResults;
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<int> GetTotalAsync()
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                var count = ctx.PersonalDataSet.Count();
                return count;
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task CreateAsync(IPersonalData pd, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var entity = PersonalDataPostgresEntity.Create(pd);
                entity.Encode(initKey);

                await ctx.PersonalDataSet.AddAsync(entity);
                await ctx.SaveChangesAsync();
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<IPersonalData> GetByIdAsync(string id, byte[] initKey)
        {
            return await GetEntityAsync(id, initKey);
        }


        public async Task ConfirmAsync(string id, DateTime confirm, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var entity = await GetEntityAsync(id, initKey);

                if (entity == null)
                    throw new Exception($"Entity not found with id: {id}");
                
                entity.Confirm = confirm;

                entity.Encode(initKey);

                ctx.PersonalDataSet.Update(entity);
                await ctx.SaveChangesAsync();
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        
        public async Task ConfirmPhoneAsync(string id, DateTime confirmPhone, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var entity = await GetEntityAsync(id, initKey);

                if (entity == null)
                    throw new Exception($"Entity not found with id: {id}");
                
                entity.ConfirmPhone = confirmPhone;

                entity.Encode(initKey);

                ctx.PersonalDataSet.Update(entity);
                await ctx.SaveChangesAsync();
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task UpdateKycAsync(string id, PersonalDataKYCEnum kyc, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var entity = await GetEntityAsync(id, initKey);

                if (entity == null)
                    throw new Exception($"Entity not found with id: {id}");

                entity.KYC = kyc;

                entity.Encode(initKey);
            
                ctx.PersonalDataSet.Update(entity);
                await ctx.SaveChangesAsync();
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task UpdateAsync(IPersonalDataUpdate update, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var entity = await GetEntityAsync(update.Id, initKey);
                
                if (entity == null)
                    throw new Exception($"Entity not found with id: {update.Id}");

                if (update.FirstName != null)
                    entity.FirstName = update.FirstName;
            
                if (update.LastName != null)
                    entity.LastName = update.LastName;
            
                if (update.City != null)
                    entity.City = update.City;

                if (update.Phone != null)
                    entity.Phone = update.Phone;

                if (update.PostalCode != null)
                    entity.PostalCode = update.PostalCode;

                if (update.CountryOfCitizenship != null)
                    entity.CountryOfCitizenship = update.CountryOfCitizenship;

                if (update.CountryOfResidence != null)
                    entity.CountryOfResidence = update.CountryOfResidence;

                if (update.Sex != null)
                    entity.Sex = update.Sex;
            
                if (update.BirthDay != null)
                    entity.DateOfBirth = update.BirthDay;

                if (update.Address != null)
                    entity.Address = update.Address;
            
                if (update.UsCitizen != null)
                    entity.USCitizen = update.UsCitizen;
                
                if (update.PhoneCode != null)
                    entity.PhoneCode = update.PhoneCode;
                
                if (update.PhoneNumber != null)
                    entity.PhoneNumber = update.PhoneNumber;

                if (update.PhoneIso != null)
                    entity.PhoneIso = update.PhoneIso;
                
                if (update.PhoneNational != null)
                    entity.PhoneNational = update.PhoneNational;
                
                entity.Encode(initKey);

                ctx.PersonalDataSet.Update(entity);
                await ctx.SaveChangesAsync();
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<IPersonalData>> GetByKycStatus(PersonalDataKYCEnum kycStatus, byte[] initKey)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var entities = (kycStatus == PersonalDataKYCEnum.NotVerified)
                    ? ctx.PersonalDataSet.Where(t => t.KYC == kycStatus || t.KYC == null).ToList()
                    : ctx.PersonalDataSet.Where(t => t.KYC == kycStatus).ToList();
                
                return entities.Select(item =>
                {
                    item.Decode(initKey);
                    return item;
                });
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        
            
        public async Task<IEnumerable<PersonalDataPostgresEntity>> SearchByIds(string searchText,
            byte[] initKey)
        {
            await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
            var entities = ctx.PersonalDataSet.Where(t => t.Id.Contains(searchText)).ToList();

            var decoded = entities.Select(item =>
            {
                item.Decode(initKey);
                return item;
            }).ToList();
            
            return decoded;
        }

        public async Task<int> GetTotalByDateAsync(DateTime from, DateTime to)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                var count = ctx.PersonalDataSet
                    .Count(t => (t.CreatedAt >= from && t.CreatedAt <= to));

                return count;
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<IPersonalData> DeactivateClientAsync(string clientId, byte[] encodingKey)
        {
            await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
            var entity = await GetEntityAsync(clientId, encodingKey);
            
            if (entity == null)
                return null;
            
            if (entity.IsDeactivated)
                return entity;
            
            entity.EmailHash = string.Empty;
            entity.Email = $"{entity.Email}_deactivated_{DateTime.UtcNow.UnixTime()}";
            entity.DeactivatedPhone = entity.Phone;
            entity.IsDeactivated = true;
            entity.Phone = String.Empty;
            entity.PhoneCode = String.Empty;
            entity.PhoneIso = String.Empty;
            entity.PhoneNational = String.Empty;
            entity.PhoneNumber = String.Empty;
            
            entity.Encode(encodingKey);
            
            ctx.PersonalDataSet.Update(entity);
            await ctx.SaveChangesAsync();

            return entity;
        }
    }
}