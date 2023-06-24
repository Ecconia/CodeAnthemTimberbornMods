using System.Collections.Generic;
using TB_CameraTweaker.Camera_Position_Manager;
using TB_CameraTweaker.CameraPositions.Store;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.Models;
using TimberApi.UiBuilderSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.UI.Position_Manager
{
    internal class CameraPositionUI_Table : UIMenuElement
    {
        private readonly CameraPositionManagerCore _core;
        private CameraPositionStore Store => _core.Store;
        private VisualElementBuilder _builder;

        public CameraPositionUI_Table(CameraPositionManagerCore core) {
            _core = core;
        }

        public override void Load() {
            _uiPriorityOrder = 200;
            base.Load();
        }

        protected override void GenerateUIContent(VisualElementBuilder builder) {
            //_builder = builder;

            ////_builder.AddComponent(builder => builder.SetFlexDirection(FlexDirection.Row).SetJustifyContent(Justify.SpaceBetween)
            //_builder.AddPreset(factory => factory.Labels().DefaultHeader("preview.listview.color", builder: builder => builder.SetStyle(style => { style.alignSelf = Align.Center; style.marginBottom = new Length(10, LengthUnit.Pixel); })));
            //_builder.AddPreset(factory => factory.ListViews().CustomListView(Store.SavedCameraPositions,
            //    CameraPositionItem,
            //    AddCameraRow
            //));
            //_builder.Build();
        }

        //private VisualElement AddCameraRow() {
        //    VisualElement root = new VisualElement();

        //    foreach (var camPos in Store.SavedCameraPositions) {
        //        VisualElement row = new VisualElement();
        //        AddCameraRow(row, camPos);
        //        root.Add(row);
        //    }
        //    return root;
        //}

        private void AddCameraRow(VisualElement visualElement, int i) {
            CameraPositionInfo camPos = Store.SavedCameraPositions[i];
            Plugin.Log.LogDebug("Adding row");

            Plugin.Log.LogDebug("Adding row 1");
            visualElement.Q<Label>("ItemLabel").text = camPos.Name;
            //visualElement.Q<Label>("ItemLabel").text = "Name";
            Plugin.Log.LogDebug("Adding row 2");
            //visualElement.Q<Button>("ActivateButton").text = "Activate";
            //Plugin.Log.LogDebug("Adding row 3");
            //visualElement.Q<Button>("DeleteButton").text = "X";
            Plugin.Log.LogDebug("Adding row 4");

            //visualElement.Q<Label>("ItemLabel").text = camPos.Name + " Vector: " + camPos.Target;

            //visualElement.Q<Button>("ActivateButton").text = "Activate";
            //visualElement.Q<Button>("ActivateButton").text = "Activate";
            //visualElement.Q<Button>("ActivateButton").clicked += () => ButtonActivateClicked(camPos);

            //visualElement.Q<Button>("DeleteButton").text = "Delete";
            //visualElement.Q<Button>("DeleteButton").clicked += () => ButtonDeleteClicked(camPos);
        }

        private void ButtonActivateClicked(CameraPositionInfo cam) {
            Plugin.Log.LogDebug("Activate Camera: " + cam.Name);
            //Store.RemoveCameraPosition(cam);
        }

        private void ButtonDeleteClicked(CameraPositionInfo cam) {
            Plugin.Log.LogDebug("Delete Camera: " + cam.Name);
            //Store.RemoveCameraPosition(cam);
        }

        private VisualElement CameraPositionItem() {
            Plugin.Log.LogDebug("Creating item");
            VisualElement item = _builder
                .AddClass(TimberApiStyle.ListViews.Hover.BgPixel3Hover)
                .AddClass(TimberApiStyle.ListViews.Selected.BgPixel3Selected)
                .SetJustifyContent(Justify.Center)
                .SetAlignItems(Align.Center)
                .SetMargin(new Margin(10, 0))
                .AddPreset(factory => factory.Labels().DefaultText(name: "ItemLabel"))
                //.AddPreset(factory => factory.Buttons().Button(name: "ActivateButton"))
                //.AddPreset(factory => factory.Buttons().Button(name: "DeleteButton"))
                .Build();
            return item;
        }

        private void OnSelectionChange(IEnumerable<object> selectedItemObjects) {
            foreach (object itemObject in selectedItemObjects) {
                if (itemObject is not CameraPositionInfo item)
                    return;

                Plugin.Log.LogInfo($"Selected item: " + item.Name);
            }
        }
    }
}