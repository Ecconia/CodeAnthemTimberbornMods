using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaks.KsHelperLib.Localization
{
    [Configurator(SceneEntrypoint.MainMenu)]
    internal class TocConfiguration : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<TocManager>().AsSingleton();
        }
    }
}