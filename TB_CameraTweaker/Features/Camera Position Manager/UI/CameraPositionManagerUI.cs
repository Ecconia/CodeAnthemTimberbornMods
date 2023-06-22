using System;
using TB_CameraTweaker.CameraPositions.Store;
using TB_CameraTweaker.KsHelperLib.UI.Menu;
using TB_CameraTweaker.Models;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.Length.Unit;

namespace TB_CameraTweaker.Features.Camera_Position_Manager.UI
{
    internal class CameraPositionManagerUI
    {
        private readonly OptionsMenu _optionsMenu;
        private readonly ICameraPositionStore _store;

        public CameraPositionManagerUI(ICameraPositionStore store) {
            _optionsMenu = DependencyContainer.GetInstance<OptionsMenu>();
            _optionsMenu.UpdateUIContent -= UpdateUIContent;
            _optionsMenu.UpdateUIContent += UpdateUIContent;
            _store = store;
        }

        private void UpdateUIContent(VisualElementBuilder builder) {
            var foo = builder.CreateComponentBuilder()
                  .CreateVisualElement();

            var parent = builder.;
            builder.AddPreset(x => x.ScrollViews().MainScrollView(parent));
            GetCameraRows(builder);

            //(x => GetCameraRows(x.Build()));
            //fac.q

            //(x => x.ScrollViews().MainScrollView());
            //fac.

            //Plugin.Log.LogDebug("Generating Camera Manager UI");
            //VisualElement root = builder.Build();
            //Plugin.Log.LogDebug("Generating Camera Manager UI 2");
            //var _savedCamerasScrollView = root.Q<ScrollView>("Mods");
            //Plugin.Log.LogDebug("Generating Camera Manager UI 3");
            //GetCameraRows(_savedCamerasScrollView);
            //Plugin.Log.LogDebug("Generating Camera Manager UI 4");
            //builder.AddComponent(_savedCamerasScrollView);
            //Plugin.Log.LogDebug("Generating Camera Manager UI 5");
        }

        private VisualElement GetCameraRows(VisualElementBuilder builder) {
            foreach (var cam in _store.SavedCameraPositions) {
                builder.AddComponent(builder.SetName(cam.Name)
                    .SetHeight(new Length(30, Pixel))
                    .SetWidth(new Length(290, Pixel))
                    .SetPadding(new Padding(0, 0, 0, 0))
                    ).Build();

                //Plugin.Log.LogDebug("2");
                //VisualElement row = list.Q<VisualElement>();
                //row.Q<Label>("Name").text = cam.Name;
                //Plugin.Log.LogDebug("3");

                //var activateButton = row.Q<Button>();
                //activateButton.name = "Activate";
                //activateButton.clicked += ButtonActivateClicked(cam);
                //row.Add(activateButton);
                //Plugin.Log.LogDebug("4");

                //var deletetButton = row.Q<Button>();
                //deletetButton.name = "Delete";
                //deletetButton.clicked += ButtonDeleteClicked(cam);
                //row.Add(deletetButton);
                //Plugin.Log.LogDebug("5");

                //list.Add(row);
                //Plugin.Log.LogDebug("6");
            }
            return list;
        }

        private Action ButtonActivateClicked(CameraPositionInfo cam) {
            throw new NotImplementedException();
        }

        private Action ButtonDeleteClicked(CameraPositionInfo cam) {
            throw new NotImplementedException();
        }
    }
}