using MyJetWallet.Sdk.Postgres;

namespace Service.PersonalData.Postgres.DesignTime
{
    public class ContextFactory : MyDesignTimeContextFactory<DatabaseContext>
    {
        public ContextFactory() : base(options => new DatabaseContext(options))
        {
        }
    }
}