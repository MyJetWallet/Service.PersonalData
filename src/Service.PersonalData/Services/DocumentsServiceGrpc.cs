using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyAzureBlob;
using Service.PersonalData.Grpc;
using Service.PersonalData.Grpc.Contracts;
using Service.PersonalData.Grpc.Documents;
using Service.PersonalData.Mappers;
using SimpleTrading.Common.Helpers;

namespace Service.PersonalData.Services
{
    public class DocumentsServiceGrpc : IDocumentsServiceGrpc
    {

        private readonly TraderDocumentsPostgresRepository _traderDocumentsRepository;
        private readonly ILogger<DocumentsServiceGrpc> _logger;
        private readonly IAzureBlobContainer _azureBlobContainer;
        public DocumentsServiceGrpc(TraderDocumentsPostgresRepository traderDocumentsRepository, ILogger<DocumentsServiceGrpc> logger, IAzureBlobContainer azureBlobContainer)
        {
            _traderDocumentsRepository = traderDocumentsRepository;
            _logger = logger;
            _azureBlobContainer = azureBlobContainer;
        }

        public async ValueTask<TraderDocumentGrpcModel> SaveDocumentAsync(UploadDocumentGrpcContract request)
        {
            try
            {
                var model = request.ToDomain();

                await _traderDocumentsRepository.Add(model);

                var encodedFileContent = AesEncodeDecode.Encode(request.Data, Program.EncodingKey);

                await _azureBlobContainer.UploadToBlobAsync(model.Id, encodedFileContent);
            
                // await ServiceLocator.AuditLogGrpcService.DispatchAuditLogsAction(request.TraderId, "kyc",
                //     model.Id,
                //     $"Trader uploaded a document with id: {model.Id}");

                return model.ToGrpc();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        public async ValueTask<GetDocumentContentGrpcResponse> DownloadDocumentAsync(DocumentGrpcContract request)
        {
            try
            {
                var document = await _traderDocumentsRepository.GetDocumentsAsync(request.TraderId, request.Id);

                if (document == null) return 
                    new GetDocumentContentGrpcResponse 
                    {
                        DocumentContent = null
                    };
                
                var fileContent = await _azureBlobContainer.DownloadBlobAsync(document.Id);
                var encodedFileContent = AesEncodeDecode.Decode(fileContent.ToArray(), Program.EncodingKey);

                return new GetDocumentContentGrpcResponse
                {
                    DocumentContent = new TraderDocumentContentGrpcModel
                    {
                        Mime = document.Mime,
                        Data = encodedFileContent,
                        FileName = document.FileName
                    }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        public async ValueTask DeleteDocumentAsync(DocumentGrpcContract request)
        {
            try
            {
                await _traderDocumentsRepository.DeleteAsync(request.TraderId, request.Id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        public async ValueTask<GetDocumentsByUserResponse> DocumentsByUserAsync(GetDocumentsByUserContract request)
        {
            try
            {
                var documents = await _traderDocumentsRepository.GetDocumentsAsync(request.TraderId);

                var result = documents.Select(x => x.ToGrpc());
                
                return new GetDocumentsByUserResponse
                {
                    Documents = result.ToArray()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                Console.WriteLine(e);
                throw;
            }
        }
    }
}