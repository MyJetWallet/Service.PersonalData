using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.PersonalData.Settings
{
    public class SettingsModel
    {
        [YamlProperty("PersonalData.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("PersonalData.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("PersonalData.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
        
        [YamlProperty("PersonalData.AzureStoragePdConnString")]
        public string AzureStoragePdConnString { get; set; }

        [YamlProperty("PersonalData.AuditLogServiceUrl")]
        public string AuditLogServiceUrl { get; set; }

        [YamlProperty("PersonalData.PostgresConnectionString")]
        public string PostgresConnectionString { get; set; }

        [YamlProperty("PersonalData.InternalAccountPatterns")]
        public string InternalAccountPatterns { get; set; }

        [YamlProperty("PersonalData.KycBlobContainerName")]
        public string AzureKycBlobContainerName { get; set; }

        [YamlProperty("PersonalData.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }
    }
}
