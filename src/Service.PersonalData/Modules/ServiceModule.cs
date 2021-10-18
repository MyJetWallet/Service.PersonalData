using System;
using System.Text;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Microsoft.WindowsAzure.Storage;
using MyAzureBlob;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using Service.AuditLog.Client;
using Service.PersonalData.Domain.Models.ServiceBus;
using Service.PersonalData.Services;

namespace Service.PersonalData.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var storageAccount = CloudStorageAccount.Parse(Program.Settings.AzureStoragePdConnString);

            builder.RegisterInstance(new MyAzureBlobContainer(storageAccount, Program.Settings.AzureKycBlobContainerName))
                .As<IAzureBlobContainer>()
                .SingleInstance();
            
            builder.RegisterAuditLogClient(Program.Settings.AuditLogServiceUrl);
            var spotServiceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), Program.LogFactory);
            builder.RegisterMyServiceBusPublisher<PersonalDataUpdateMessage>(spotServiceBusClient, PersonalDataUpdateMessage.TopicName, false);
            
            
            builder.RegisterType<PersonalDataCache>().AsSelf().SingleInstance();
            builder.RegisterType<PersonalDataPostgresRepository>().AsSelf().SingleInstance();
            builder.RegisterType<TraderDocumentsPostgresRepository>().AsSelf().SingleInstance();
        }
    }
}