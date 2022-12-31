using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaks.Patchers
{
    [Configurator(SceneEntrypoint.MainMenu)]
    internal class PatchConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<CameraZoomPatcher>().AsSingleton();
        }
    }
}