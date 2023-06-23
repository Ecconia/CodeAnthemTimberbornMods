using Bindito.Core;
using TB_CameraTweaker.Camera_Position_Manager;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.Features
{
    [Configurator(SceneEntrypoint.All)]
    internal class FeatureConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            containerDefinition.Bind<CameraPositionManagerCore>().AsSingleton();
        }
    }
}