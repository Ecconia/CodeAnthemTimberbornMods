using Bindito.Core;
using TB_CameraTweaks.UI;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using Timberborn.Localization;

namespace TB_CameraTweaks.Patchers
{
    [Configurator(SceneEntrypoint.All)]
    internal class PatchConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<CameraZoomPatcher>().AsSingleton();
        }
    }
}