using MyNoSqlServer.Abstractions;
using Service.PersonalData.Domain.Models.NoSql;
using Service.PersonalData.Grpc;
using Service.PersonalData.Grpc.Contracts;
using Service.PersonalData.Grpc.Models;
using System.Threading.Tasks;

// ReSharper disable UnusedMember.Global

namespace Service.PersonalData.Client
{
    public class BillingDeailsServiceDecorator : IBillingDetailsServiceGrpc
    {
        private readonly IBillingDetailsServiceGrpc _billingDetailsServiceGrpc;
        private readonly IMyNoSqlServerDataReader<BillingDetailsNoSql> _reader;

        public BillingDeailsServiceDecorator(IBillingDetailsServiceGrpc billingDetailsServiceGrpc, IMyNoSqlServerDataReader<BillingDetailsNoSql> reader)
        {
            this._billingDetailsServiceGrpc = billingDetailsServiceGrpc;
            this._reader = reader;
        }

        public async ValueTask<GetBillingDetailsGrpcResponse> GetAsync(GetBillingDetailsGrpcModel request)
        {
            var detailsEncrypted = _reader.Get(BillingDetailsNoSql.GeneratePartitionKey(request.ClientId), BillingDetailsNoSql.GeneratePartitionKey(request.Uid));

            if (detailsEncrypted != null)
            {
                try
                {
                    var details = detailsEncrypted.BillingDetails.GetUnEncrypted(System.Text.Encoding.UTF8.GetBytes(request.Secret));

                    return new GetBillingDetailsGrpcResponse
                    {
                        BillingDetails = details
                    };
                }
                catch (System.Exception)
                {
                }
            }

            var res = await _billingDetailsServiceGrpc.GetAsync(request);
            {
                try
                {
                    var details = res.BillingDetails
                    ?.GetUnEncrypted(System.Text.Encoding.UTF8.GetBytes(request.Secret));

                    if (details == null)
                    {
                        return new GetBillingDetailsGrpcResponse
                        {
                            BillingDetails = null
                        };
                    }

                    return new GetBillingDetailsGrpcResponse
                    {
                        BillingDetails = details
                    };
                }
                catch
                {
                    return new GetBillingDetailsGrpcResponse
                    {
                        BillingDetails = null
                    };
                }
            }
        }

        public ValueTask<ResultGrpcResponse> SetAsync(SetBillingDetailsGrpcModel personalData)
        {
            return _billingDetailsServiceGrpc.SetAsync(personalData);
        }
    }
}
