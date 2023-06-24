using Bindito.Core;
using TB_CameraTweaker.UI.Tweaks;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.UI
{
    [Configurator(SceneEntrypoint.All)]
    internal class UIConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            // tweaks
            containerDefinition.Bind<CameraTweakerUI_SettingsTitle>().AsSingleton();
            containerDefinition.Bind<CameraTweakerUI_FOV>().AsSingleton();
            //containerDefinition.Bind<CameraTweakerUI_VerticalAngleLimiter>().AsSingleton();
            containerDefinition.Bind<CameraTweakerUI_ZoomLevelLimiter>().AsSingleton();
            containerDefinition.Bind<CameraTweakerUI_ZoomSpeed>().AsSingleton();

            // position manager
            //containerDefinition.Bind<CameraPositionUI_SettingsTitle>().AsSingleton();
            //containerDefinition.Bind<CameraPositionUI_Freeze>().AsSingleton();
            //containerDefinition.Bind<CameraPositionUI_Table>().AsSingleton();
        }
    }
}