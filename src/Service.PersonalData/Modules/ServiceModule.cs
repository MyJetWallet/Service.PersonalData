using Autofac;
using Microsoft.WindowsAzure.Storage;
using MyAzureBlob;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.ServiceBus;
using Service.ClientAuditLog.Domain.Models;
using Service.PersonalData.Domain.Models.NoSql;
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
            
            var spotServiceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), Program.LogFactory);
            builder.RegisterMyServiceBusPublisher<PersonalDataUpdateMessage>(spotServiceBusClient, PersonalDataUpdateMessage.TopicName, false);
            builder.RegisterMyServiceBusPublisher<ClientAuditLogModel>(spotServiceBusClient, ClientAuditLogModel.TopicName, false);

            builder.RegisterType<PersonalDataCache>().AsSelf().SingleInstance();
            builder.RegisterType<PersonalDataPostgresRepository>().AsSelf().SingleInstance();
            builder.RegisterType<TraderDocumentsPostgresRepository>().AsSelf().SingleInstance();

            builder.RegisterMyNoSqlWriter<BillingDetailsNoSql>(
               Program.ReloadedSettings(e => e.MyNoSqlWriterUrl),
               BillingDetailsNoSql.TableName);
        }
    }
}