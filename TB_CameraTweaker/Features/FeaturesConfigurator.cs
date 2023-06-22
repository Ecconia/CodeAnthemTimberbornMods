using Bindito.Core;
using TB_CameraTweaker.Features.Camera_Position_Manager;
using TB_CameraTweaker.Features.Camera_Tweaker.UI;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.Features
{
    [Configurator(SceneEntrypoint.All)]
    internal class FeaturesConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            containerDefinition.Bind<CameraTweakerUI_FOV>().AsSingleton();
            containerDefinition.Bind<CameraTweakerUI_Freeze>().AsSingleton();
            containerDefinition.Bind<CameraTweakerUI_VerticalAngleLimiter>().AsSingleton();
            containerDefinition.Bind<CameraTweakerUI_ZoomLevelLimiter>().AsSingleton();
            containerDefinition.Bind<CameraTweakerUI_ZoomSpeed>().AsSingleton();
            containerDefinition.Bind<CameraPositionManagerCore>().AsSingleton();
        }
    }
}