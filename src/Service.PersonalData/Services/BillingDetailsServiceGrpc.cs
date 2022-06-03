using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using MyNoSqlServer.Abstractions;
using MyServiceBus.Abstractions;
using Newtonsoft.Json;
using Service.AuditLog.Grpc;
using Service.AuditLog.Grpc.Models;
using Service.ClientAuditLog.Domain.Models;
using Service.PersonalData.Domain.Models;
using Service.PersonalData.Domain.Models.NoSql;
using Service.PersonalData.Domain.Models.ServiceBus;
using Service.PersonalData.Grpc;
using Service.PersonalData.Grpc.Contracts;
using Service.PersonalData.Grpc.Models;
using Service.PersonalData.Mappers;
using Service.PersonalData.Postgres;
using Service.PersonalData.Postgres.Models;

namespace Service.PersonalData.Services
{
    public class BillingDetailsServiceGrpc : IBillingDetailsServiceGrpc
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        private readonly ILogger<BillingDetailsServiceGrpc> _logger;
        private readonly IMyNoSqlServerDataWriter<BillingDetailsNoSql> _writer;

        public BillingDetailsServiceGrpc(
            DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder,
            ILogger<BillingDetailsServiceGrpc> logger,
            IMyNoSqlServerDataWriter<BillingDetailsNoSql> writer)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
            _logger = logger;
            _writer = writer;
        }

        public async ValueTask<ResultGrpcResponse> SetAsync(SetBillingDetailsGrpcModel request)
        {
            try
            {
                var billingDetails = request.BillingDetails;
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var result = await ctx.BillingDetails.Upsert(new BillingDetailsEntity
                {
                    BillingCity = billingDetails.BillingCity?.EncryptString(Program.EncodingKey),
                    BillingCountry = billingDetails.BillingCountry?.EncryptString(Program.EncodingKey),
                    BillingDistrict = billingDetails.BillingDistrict?.EncryptString(Program.EncodingKey),
                    BillingLine1 = billingDetails.BillingLine1?.EncryptString(Program.EncodingKey),
                    BillingLine2 = billingDetails.BillingLine2?.EncryptString(Program.EncodingKey),
                    BillingName = billingDetails.BillingName?.EncryptString(Program.EncodingKey),
                    BillingPostalCode = billingDetails.BillingPostalCode?.EncryptString(Program.EncodingKey),
                    ClientId = billingDetails.ClientId,
                }).On(x => x.ClientId).RunAsync();

                var cache = await _writer.GetAsync(BillingDetailsNoSql.GeneratePartitionKey(billingDetails.ClientId));

                if (cache != null)
                    foreach (var item in cache)
                    {
                        await _writer.DeleteAsync(item.PartitionKey, item.RowKey);
                    }

                return new ResultGrpcResponse { Ok = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new ResultGrpcResponse { Ok = false };
            }
        }

        public async ValueTask<GetBillingDetailsGrpcResponse> GetAsync(GetBillingDetailsGrpcModel request)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);

                var billingDetails = await ctx.BillingDetails.FirstOrDefaultAsync(x => x.ClientId == request.ClientId);

                if (billingDetails == null)
                    return new GetBillingDetailsGrpcResponse
                    {
                        BillingDetails = null

                    };

                var unencrypted = new BillingDetails
                {
                    ClientId = request.ClientId,
                    BillingCity = billingDetails.BillingCity?.DecryptString(Program.EncodingKey),
                    BillingCountry = billingDetails.BillingCountry?.DecryptString(Program.EncodingKey),
                    BillingDistrict = billingDetails.BillingDistrict?.DecryptString(Program.EncodingKey),
                    BillingLine1 = billingDetails.BillingLine1?.DecryptString(Program.EncodingKey),
                    BillingLine2 = billingDetails.BillingLine2?.DecryptString(Program.EncodingKey),
                    BillingName = billingDetails.BillingName?.DecryptString(Program.EncodingKey),
                    BillingPostalCode = billingDetails.BillingPostalCode?.DecryptString(Program.EncodingKey),
                };

                var encrypted = unencrypted.GetEncrypted(System.Text.Encoding.UTF8.GetBytes(request.Secret));
                await _writer.InsertOrReplaceAsync(BillingDetailsNoSql.Create(unencrypted.ClientId, request.Uid, encrypted));

                return new GetBillingDetailsGrpcResponse { BillingDetails = encrypted };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new GetBillingDetailsGrpcResponse { BillingDetails = null };
            }
        }
    }
}