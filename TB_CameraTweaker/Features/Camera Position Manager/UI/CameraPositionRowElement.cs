using TB_CameraTweaker.Features.Camera_Position_Manager;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.KsHelperLib.UI.Elements.CameraPositionRow
{
    internal class CameraPositionRowElement
    {
        private readonly CameraPositionManagerCore _core;
        private readonly bool _isActive = false;

        public CameraPositionRowElement(string positionName, CameraPositionManagerCore cameraPositionSaveSystemCore) {
            PositionName = positionName;
            _core = cameraPositionSaveSystemCore;
            if (_core.IsPositionActive(PositionName)) { _isActive = true; }
        }

        public string PositionName { get; private set; }

        public void Build(VisualElementBuilder builder) {
            //MakeLabel(builder);
            MakeButton(builder);
            //FooterNote(builder);
        }

        private void ActivateButtonClicked() => _core.SetActiveCameraPosition(PositionName);

        private void DeleteButtonClicked() => _core.RemoveCameraPosition(PositionName);

        private void MakeButton(VisualElementBuilder builder) {
            builder
                .AddPreset(factory => factory.Buttons().Button(name: "Button Name", text: "X", builder: builder => builder.SetStyle(style => {
                    style = CreateButtonStyle(style);
                    style.width = 50;

                    style.color = Color.red;
                })
                .ModifyElement(x => x.clicked += () => DeleteButtonClicked())
                ))
                .AddPreset(factory => factory.Buttons().Button(name: "Button Name", text: "Activate", builder: builder => builder.SetStyle(style => {
                    style = CreateButtonStyle(style);
                    style.color = Color.blue;
                })
                .ModifyElement(x => x.clicked += () => ActivateButtonClicked())
                ));
        }

        private IStyle CreateButtonStyle(IStyle s) {
            s.alignSelf = Align.FlexEnd;
            s.flexShrink = 5;
            s.flexDirection = FlexDirection.Row;
            s.justifyContent = Justify.FlexEnd;
            s.width = 100;
            s.flexWrap = Wrap.NoWrap;
            s.backgroundColor = _isActive ? Color.green : Color.gray;
            return s;
        }
    }
}