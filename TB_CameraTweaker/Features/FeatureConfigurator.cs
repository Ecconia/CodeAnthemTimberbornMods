using Bindito.Core;
using TB_CameraTweaker.Camera_Position_Manager;
using TB_CameraTweaker.CameraPositions.Store;
using TB_CameraTweaker.Features.Camera_Freeze;
using TB_CameraTweaker.KsHelperLib.DataSaver;
using TB_CameraTweaker.Models;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace TB_CameraTweaker.Features
{
    [Configurator(SceneEntrypoint.All)]
    internal class FeatureConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition) {
            // Freezer
            containerDefinition.Bind<CameraPositionFreezer>().AsSingleton();

            // Camera Position Manager
            containerDefinition.Bind<CameraPositionManagerCore>().AsSingleton();
            containerDefinition.Bind<IDataSaver<CameraPositionInfo>>().To<JsonFileDataSaver<CameraPositionInfo>>().AsSingleton();
            containerDefinition.Bind<CameraPositionStore>().AsSingleton();
        }
    }
}