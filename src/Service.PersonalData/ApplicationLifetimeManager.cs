using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyServiceBus.TcpClient;
using Service.PersonalData.Services;

namespace Service.PersonalData
{
    public class ApplicationLifetimeManager : ApplicationLifetimeManagerBase
    {
        private readonly ILogger<ApplicationLifetimeManager> _logger;
        private readonly MyServiceBusTcpClient _myServiceBusTcpClient;
        private readonly PersonalDataCache _personalDataCache;

        public ApplicationLifetimeManager(IHostApplicationLifetime appLifetime, ILogger<ApplicationLifetimeManager> logger, MyServiceBusTcpClient myServiceBusTcpClient, PersonalDataCache personalDataCache)
            : base(appLifetime)
        {
            _logger = logger;
            _myServiceBusTcpClient = myServiceBusTcpClient;
            _personalDataCache = personalDataCache;
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
            _myServiceBusTcpClient.Start();
            _personalDataCache.Start();
        }

        protected override void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
            _myServiceBusTcpClient.Stop();

        }

        protected override void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}
