using System.ServiceModel;
using System.Threading.Tasks;
using Service.PersonalData.Grpc.Contracts;
using Service.PersonalData.Grpc.Models;

namespace Service.PersonalData.Grpc
{
    [ServiceContract]
    public interface IBillingDetailsServiceGrpc
    {
        [OperationContract]
        ValueTask<ResultGrpcResponse> SetAsync(SetBillingDetailsGrpcModel personalData);

        [OperationContract]
        ValueTask<GetBillingDetailsGrpcResponse> GetAsync(GetBillingDetailsGrpcModel personalData);

    }
}