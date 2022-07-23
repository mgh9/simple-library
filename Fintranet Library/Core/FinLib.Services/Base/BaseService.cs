namespace FinLib.Services.Base
{
    public abstract partial class BaseService
    {
        protected ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> CommonServicesProvider { get; }

        protected BaseService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
        {
            CommonServicesProvider = commonServicesProvider;
        }
    }
}
