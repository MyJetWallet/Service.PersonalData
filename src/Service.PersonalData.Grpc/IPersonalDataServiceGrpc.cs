using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Service.PersonalData.Grpc.Contracts;
using Service.PersonalData.Grpc.Models;

namespace Service.PersonalData.Grpc
{
    [ServiceContract]
    public interface IPersonalDataServiceGrpc
    {
        [OperationContract]
        ValueTask<ResultGrpcResponse> RegisterAsync(RegisterPersonalDataGrpcModel personalData);
        
        [OperationContract]
        ValueTask<ResultGrpcResponse> UpdateAsync(UpdatePersonalDataGrpcContract personalData);
        
        [OperationContract]
        ValueTask<PersonalDataGrpcResponseContract> GetByIdAsync(GetByIdRequest request);

        [OperationContract]
        ValueTask<PersonalDataBatchResponseContract> GetByIdsAsync(GetByIdsRequest request);

        [OperationContract]
        ValueTask<PersonalDataGrpcResponseContract> GetByEmail(GetByEmailRequest request);

        [OperationContract]
        ValueTask ConfirmAsync(ConfirmGrpcModel confirmData);
        
        [OperationContract]
        ValueTask ConfirmPhoneAsync(ConfirmGrpcModel confirmData);
        
        [OperationContract]
        ValueTask UpdateKycAsync(UpdateKycGrpcContract request);
        
        [OperationContract]
        ValueTask<GetPersonalDataByStatusResponse> GetPersonalDataByStatus(GetPersonalDataByStatusRequest request);

        [OperationContract]
        ValueTask<PersonalDataBatchResponseContract> GetAsync(GetRequest request);

        [OperationContract]
        ValueTask<GetTotalResponse> GetTotalAsync();
        
        [OperationContract]
        ValueTask<PersonalDataGrpcResponseContract> GetByPhone(GetByPhoneRequest request);
        
        [OperationContract]
        ValueTask<PersonalDataBatchResponseContract> SearchAsync(SearchRequest request);
    }
}