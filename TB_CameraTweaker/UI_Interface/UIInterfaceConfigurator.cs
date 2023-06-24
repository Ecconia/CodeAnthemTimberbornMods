using Bindito.Core;
using TB_CameraTweaker.UI_Interface;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.UI
{
    [Configurator(SceneEntrypoint.InGame)]
    internal class UIInterfaceConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            containerDefinition.Bind<CameraPositionUI_InterfaceHandler>().AsSingleton();
            containerDefinition.Bind<CameraPositionUI_InterfacePresetFactory>().AsSingleton();
            containerDefinition.Bind<ICameraPositionUI_InterfacePanel>().To<CameraPositionUI_InterfacePanel>().AsSingleton();
        }
    }
}