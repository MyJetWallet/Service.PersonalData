using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.PersonalData.Domain.Models.ServiceBus;
using Service.PersonalData.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.PersonalData.Client
{
    public static class AutofacHelper
    {
        public static void RegisterPersonalDataClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new PersonalDataClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetPersonalDataService()).As<IPersonalDataServiceGrpc>().SingleInstance();
        }
        
        public static void RegisterTraderDocumentsClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new PersonalDataClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetDocumentsService()).As<IDocumentsServiceGrpc>().SingleInstance();
        }
        
        public static void RegisterPersonalDataUpdateSubscriber(this ContainerBuilder builder, MyServiceBusTcpClient serviceBusClient, string queue)
        {
            builder.RegisterMyServiceBusSubscriberBatch<PersonalDataUpdateMessage>(serviceBusClient, PersonalDataUpdateMessage.TopicName, queue,
                TopicQueueType.Permanent);
        }    
    }
}
