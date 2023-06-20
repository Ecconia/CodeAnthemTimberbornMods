using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.Patchers
{
    [Configurator(SceneEntrypoint.MainMenu)]
    internal class PatchConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            containerDefinition.Bind<CameraFOVPatcher>().AsSingleton();
            containerDefinition.Bind<CameraVerticalAngelLimiterPatcher>().AsSingleton();
            containerDefinition.Bind<CameraZoomPatcher>().AsSingleton();
            containerDefinition.Bind<CameraPositionDataPatcher>().AsSingleton();
        }
    }
}