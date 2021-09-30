using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.PersonalData.Grpc;

namespace Service.PersonalData.Client
{
    [UsedImplicitly]
    public class PersonalDataClientFactory: MyGrpcClientFactory
    {
        public PersonalDataClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IPersonalDataServiceGrpc GetPersonalDataService() => CreateGrpcService<IPersonalDataServiceGrpc>();
        public IDocumentsServiceGrpc GetDocumentsService() => CreateGrpcService<IDocumentsServiceGrpc>();

    }
}
