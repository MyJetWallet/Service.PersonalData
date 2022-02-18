using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using Newtonsoft.Json;
using Service.AuditLog.Grpc;
using Service.AuditLog.Grpc.Models;
using Service.PersonalData.Domain.Models.ServiceBus;
using Service.PersonalData.Grpc;
using Service.PersonalData.Grpc.Contracts;
using Service.PersonalData.Grpc.Models;
using Service.PersonalData.Mappers;
using Service.PersonalData.Postgres.Models;

namespace Service.PersonalData.Services
{
    public class PersonalDataService : IPersonalDataServiceGrpc
    {
        private readonly PersonalDataPostgresRepository _personalDataRepository;
        private readonly ILogger<PersonalDataService> _logger;
        private readonly IAuditLogServiceGrpc _auditLogService;
        private readonly PersonalDataCache _personalDataCache;
        private readonly IServiceBusPublisher<PersonalDataUpdateMessage> _publisher;

        public PersonalDataService(PersonalDataPostgresRepository personalDataRepository, ILogger<PersonalDataService> logger, IAuditLogServiceGrpc auditLogService, PersonalDataCache personalDataCache, IServiceBusPublisher<PersonalDataUpdateMessage> publisher)
        {
            _personalDataRepository = personalDataRepository;
            _logger = logger;
            _auditLogService = auditLogService;
            _personalDataCache = personalDataCache;
            _publisher = publisher;
        }

        private static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public async ValueTask<ResultGrpcResponse> RegisterAsync(RegisterPersonalDataGrpcModel request)
        {
            try
            {
                var isInternal = Regex.IsMatch(request.Email, Program.Settings.InternalAccountPatterns);

                var pd = request.ToDomainModel(isInternal);

                await _personalDataRepository.CreateAsync(pd, Program.EncodingKey);

                var logData = ToJson(pd);

                var log = new AuditLogGrpcModel
                {
                    TraderId = request.Id,
                    ProcessId = request.AuditLog.ProcessId,
                    Ip = request.AuditLog.Ip,
                    ServiceName = request.AuditLog.ServiceName,
                    Context = request.AuditLog.Context,
                    Before = string.Empty,
                    UpdatedData = logData,
                    After = logData
                };

                await _auditLogService.RegisterEventAsync(log);
                _personalDataCache.UpdateCache(pd);
                return new ResultGrpcResponse {Ok = true};
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new ResultGrpcResponse {Ok = false};
            }
        }

        public async ValueTask<ResultGrpcResponse> UpdateAsync(UpdatePersonalDataGrpcContract request)
        {
            try
            {
                var pd = await _personalDataRepository.GetByIdAsync(request.Id,
                    Program.EncodingKey);

                await _personalDataRepository.UpdateAsync(new PersonalDataPostgresUpdateEntity
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    City = request.City,
                    Phone = request.Phone,
                    PostalCode = request.PostalCode,
                    CountryOfCitizenship = request.CountryOfCitizenship,
                    CountryOfResidence = request.CountryOfResidence,
                    Address = request.Address,
                    UsCitizen = request.USCitizen,
                    Sex = request.Sex,
                    BirthDay = request.DateOfBirth,
                    PhoneCode = request.PhoneCode,
                    PhoneNumber = request.PhoneNumber,
                    PhoneIso = request.PhoneIso,
                    PhoneNational = request.PhoneNational
                }, Program.EncodingKey);

                var updatedPd =
                    await _personalDataRepository.GetByIdAsync(request.Id, Program.EncodingKey);
                
                var log = new AuditLogGrpcModel
                {
                    TraderId = request.Id,
                    ProcessId = request.AuditLog.ProcessId,
                    Ip = request.AuditLog.Ip,
                    ServiceName = request.AuditLog.ServiceName,
                    Context = request.AuditLog.Context,
                    Before = ToJson(pd),
                    UpdatedData = ToJson(request),
                    After = ToJson(updatedPd)
                };

                await _auditLogService.RegisterEventAsync(log);
                _personalDataCache.UpdateCache(updatedPd);
                
                await _publisher.PublishAsync(new PersonalDataUpdateMessage()
                {
                    TraderId = request.Id
                });

                return new ResultGrpcResponse {Ok = true};
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new ResultGrpcResponse {Ok = false};
            }
        }

        public async ValueTask ConfirmAsync(ConfirmGrpcModel request)
        {
            try
            {
                var pd = await _personalDataRepository.GetByIdAsync(request.Id,
                    Program.EncodingKey);

                await _personalDataRepository.ConfirmAsync(request.Id, request.Confirm,
                    Program.EncodingKey);

                var updatedPd =
                    await _personalDataRepository.GetByIdAsync(request.Id, Program.EncodingKey);

                var log = new AuditLogGrpcModel
                {
                    TraderId = request.Id,
                    ProcessId = string.Empty,
                    Ip = request.AuditLog.Ip,
                    ServiceName = request.AuditLog.ServiceName,
                    Context = request.AuditLog.Context,
                    Before = ToJson(pd),
                    UpdatedData = ToJson(request),
                    After = ToJson(updatedPd),
                };

                await _auditLogService.RegisterEventAsync(log);
                _personalDataCache.UpdateCache(updatedPd);
                await _publisher.PublishAsync(new PersonalDataUpdateMessage()
                {
                    TraderId = request.Id
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async ValueTask ConfirmPhoneAsync(ConfirmGrpcModel request)
        {
            try
            {
                var pd = await _personalDataRepository.GetByIdAsync(request.Id,
                    Program.EncodingKey);

                await _personalDataRepository.ConfirmPhoneAsync(request.Id, request.Confirm,
                    Program.EncodingKey);

                var updatedPd =
                    await _personalDataRepository.GetByIdAsync(request.Id, Program.EncodingKey);

                var log = new AuditLogGrpcModel
                {
                    TraderId = request.Id,
                    ProcessId = string.Empty,
                    Ip = request.AuditLog.Ip,
                    ServiceName = request.AuditLog.ServiceName,
                    Context = request.AuditLog.Context,
                    Before = ToJson(pd),
                    UpdatedData = ToJson(request),
                    After = ToJson(updatedPd),
                };
                
                await _auditLogService.RegisterEventAsync(log);
                _personalDataCache.UpdateCache(updatedPd);
                await _publisher.PublishAsync(new PersonalDataUpdateMessage()
                {
                    TraderId = request.Id
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }        
        }

        public async ValueTask UpdateKycAsync(UpdateKycGrpcContract request)
        {
            var pd = await _personalDataRepository.GetByIdAsync(request.Id, Program.EncodingKey);

            await _personalDataRepository.UpdateKycAsync(request.Id, request.Kyc,
                Program.EncodingKey);

            var updatedPd =
                await _personalDataRepository.GetByIdAsync(request.Id, Program.EncodingKey);

            var log = new AuditLogGrpcModel
            {
                TraderId = request.Id,
                ProcessId = request.AuditLog.ProcessId,
                Ip = request.AuditLog.Ip,
                ServiceName = request.AuditLog.ServiceName,
                Context = request.AuditLog.Context,
                Before = ToJson(pd),
                UpdatedData = ToJson(request.Kyc),
                After = ToJson(updatedPd),
            };

            await _auditLogService.RegisterEventAsync(log);
            _personalDataCache.UpdateCache(updatedPd);
            await _publisher.PublishAsync(new PersonalDataUpdateMessage()
            {
                TraderId = request.Id
            });
        }

        public async ValueTask<PersonalDataGrpcResponseContract> GetByIdAsync(GetByIdRequest request)
        {
            Console.WriteLine("Requesting data by ID: " + request.Id);

            var pd = await _personalDataRepository.GetByIdAsync(request.Id, Program.EncodingKey);

            var response = new PersonalDataGrpcResponseContract();

            if (pd != null)
                response.PersonalData = pd.ToGrpcModel();

            return response;
        }

        public async ValueTask<PersonalDataBatchResponseContract> GetByIdsAsync(GetByIdsRequest request)
        {
            var personalDatas = await _personalDataRepository.GetByIdsAsync(request.Ids, Program.EncodingKey);

            var response = new PersonalDataBatchResponseContract();

            if (personalDatas != null)
                response.PersonalDatas = personalDatas.Select(pd => pd.ToGrpcModel());

            return response;
        }

        public async ValueTask<PersonalDataBatchResponseContract> GetAsync(GetRequest request)
        {
            var personalDatas = await _personalDataRepository.GetAsync(request.Limit, request.Offset, 
                Program.EncodingKey);

            var response = new PersonalDataBatchResponseContract();

            if (personalDatas != null)
            {
                response.PersonalDatas = personalDatas.Select(pd => pd.ToGrpcModel());
            }

            return response;
        }

        public async ValueTask<GetTotalResponse> GetTotalAsync() =>
            new GetTotalResponse
            {
                TotalPersonalDatas = await _personalDataRepository.GetTotalAsync()
            };

        public async ValueTask<PersonalDataGrpcResponseContract> GetByEmail(GetByEmailRequest request)
        {
            var response = new PersonalDataGrpcResponseContract();
            var id = _personalDataCache.GetUserIdByEmail(request.Email);
            if (!string.IsNullOrEmpty(id))
            {
                var entity =
                    await _personalDataRepository.GetByIdAsync(id, Program.EncodingKey);
                if (entity != null)
                    response.PersonalData = entity.ToGrpcModel();
            }
            return response;
        }
        
        public async ValueTask<PersonalDataGrpcResponseContract> GetByPhone(GetByPhoneRequest request)
        {
            var response = new PersonalDataGrpcResponseContract();
            var id = _personalDataCache.GetUserIdByPhone(request.Phone);
            if (!string.IsNullOrEmpty(id))
            {
                var entity =
                    await _personalDataRepository.GetByIdAsync(id, Program.EncodingKey);
                if (entity != null)
                    response.PersonalData = entity.ToGrpcModel();
            }
            return response;
        }

        public async ValueTask<PersonalDataBatchResponseContract> SearchAsync(SearchRequest request)
        {
            var ids = _personalDataCache.Search(request.SearchText);

            var entitiesFromCache = await _personalDataRepository.GetByIdsAsync(ids, Program.EncodingKey);
            var entities = await _personalDataRepository.SearchByIds(request.SearchText, Program.EncodingKey);

            return new PersonalDataBatchResponseContract()
            {
                PersonalDatas = entities.Concat(entitiesFromCache).DistinctBy(t=>t.Id).Select(pd => pd.ToGrpcModel())
            };

        }

        public async ValueTask<ResultGrpcResponse> CreateRecordAsync(PersonalDataGrpcModel request)
        {
            _logger.LogInformation("Creating personal data record for user id {userId}", request.Id);
            try
            {
                var pd = request.ToDomainModel();

                await _personalDataRepository.CreateAsync(pd, Program.EncodingKey);

                var logData = ToJson(pd);

                var log = new AuditLogGrpcModel
                {
                    TraderId = request.Id,
                    ProcessId = request.AuditLog.ProcessId,
                    Ip = request.AuditLog.Ip,
                    ServiceName = request.AuditLog.ServiceName,
                    Context = request.AuditLog.Context,
                    Before = string.Empty,
                    UpdatedData = logData,
                    After = logData
                };

                await _auditLogService.RegisterEventAsync(log);
                _personalDataCache.UpdateCache(pd);
                await _publisher.PublishAsync(new PersonalDataUpdateMessage()
                {
                    TraderId = request.Id
                });
                return new ResultGrpcResponse {Ok = true};
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new ResultGrpcResponse {Ok = false};
            }
        }
    }
}