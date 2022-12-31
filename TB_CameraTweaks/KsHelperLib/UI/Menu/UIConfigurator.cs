using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaks.KsHelperLib.UI.Menu
{
    [Configurator(SceneEntrypoint.All)]
    internal class UIConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<OptionsMenu>().AsSingleton();
        }
    }
}