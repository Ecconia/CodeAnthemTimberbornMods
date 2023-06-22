using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.KsHelperLib.UI.Menu
{
    [Configurator(SceneEntrypoint.All)]
    internal class OptionsConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            containerDefinition.Bind<OptionsMenu>().AsSingleton();
        }
    }
}