using Bindito.Core;
using TB_CameraTweaker.KsHelperLib.Patches;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.Patches
{
    [Configurator(SceneEntrypoint.All)]
    internal class PatchConfiguratior : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            containerDefinition.Bind<CameraGetPositionPatcher>().AsSingleton();
            containerDefinition.Bind<CameraFOVPatcher>().AsSingleton();
            containerDefinition.Bind<CameraFreeModePatcher>().AsSingleton();
            containerDefinition.Bind<CameraSetPositionPatcher>().AsSingleton();
            containerDefinition.Bind<CameraVerticalAngleLimiterPatcher>().AsSingleton();
            containerDefinition.Bind<CameraZoomLevelLimitPatcher>().AsSingleton();
            containerDefinition.Bind<CameraZoomLevelPatcher>().AsSingleton();
            containerDefinition.Bind<CameraZoomSpeedPatcher>().AsSingleton();
            containerDefinition.Bind<PatchLoader>().AsSingleton();
        }
    }
}