using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.KsHelperLib.Localization
{
    [Configurator(SceneEntrypoint.MainMenu)]
    internal class LocConfiguration : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<LocManager>().AsSingleton();
        }
    }
}