using System.ServiceModel;
using System.Threading.Tasks;
using Service.PersonalData.Grpc.Contracts;
using Service.PersonalData.Grpc.Documents;

namespace Service.PersonalData.Grpc
{
    [ServiceContract]
    public interface IDocumentsServiceGrpc
    {
        [OperationContract]
        ValueTask<TraderDocumentGrpcModel> SaveDocumentAsync(UploadDocumentGrpcContract request);

        [OperationContract]
        ValueTask<GetDocumentContentGrpcResponse> DownloadDocumentAsync(DocumentGrpcContract request);
        
        [OperationContract]
        ValueTask DeleteDocumentAsync(DocumentGrpcContract request);
        
        [OperationContract]
        ValueTask<GetDocumentsByUserResponse> DocumentsByUserAsync(GetDocumentsByUserContract request);
    }
}