using System;
using TB_CameraTweaker.Camera_Position_Manager;
using TB_CameraTweaker.KsHelperLib.Localization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace TB_CameraTweaker.UI_Interface
{
    internal class CameraPositionUI_InterfaceHandler : ILoadableSingleton
    {
        private readonly ICameraPositionUI_InterfacePanel _panel;
        private readonly CameraPositionManagerCore _core;
        //private readonly LogProxy _log = new("InterfaceHandler");

        private bool _isFreezed = false;

        public CameraPositionUI_InterfaceHandler(ICameraPositionUI_InterfacePanel panel, CameraPositionManagerCore core) {
            _panel = panel ?? throw new ArgumentNullException(nameof(panel));
            _core = core ?? throw new ArgumentNullException(nameof(panel));
        }

        public void Load() {
            BindButtonHandlers();
            _core.ActiveCameraPositionChanged += UpdateButtons;
            _core.OnCameraMoved += UpdateJumpButtonColor;
            _core.GetFirst();
        }

        private void BindButtonHandlers() {
            _panel.CameraFreezeButton.clicked += () => {
                _isFreezed = _core.Freeze();
                UpdateButtons();
            };
            _panel.CameraAddButton.clicked += () => _panel.GetNamePopup($"{LocConfig.LocTag}.interface.entername", _core.AddCurrentCameraButton);
            _panel.CameraRemoveButton.clicked += _core.RemoveActiveCamera;

            _panel.CameraPreviousButton.clicked += () => { _core.SetPreviousActive(); _core.JumpToActivePosition(); };
            _panel.CameraJumpButton.clicked += _core.JumpToActivePosition;
            _panel.CameraNextButton.clicked += () => { _core.SetNextActive(); _core.JumpToActivePosition(); };
        }

        private void UpdateButtons() {
            _panel.CameraRemoveButton.SetEnabled(_core.ActivePosition != null);  // Toggle remove button if no active camera
            _panel.CameraFreezeButton.style.color = _isFreezed ? Color.red : Color.white;

            // Toggle nav button state if saved cameras is more than one
            _panel.CameraPreviousButton.SetEnabled(_core.Store.SavedCameraPositions.Count > 1 && !_isFreezed);
            _panel.CameraNextButton.SetEnabled(_core.Store.SavedCameraPositions.Count > 1 && !_isFreezed);

            // Update Jump Button Text With ActivePosition if any
            _panel.CameraJumpButton.text = _core.ActivePosition?.Name ?? "none";

            //UpdateJumpButtonText();
            UpdateJumpButtonColor();
        }

        private void UpdateJumpButtonColor() {
            if (_core.ActivePosition == null) {
                _panel.CameraJumpButton.style.color = Color.white;
                _panel.CameraJumpButton.SetEnabled(false);
                return;
            };

            bool isSamePosition = _core.IsCurrentPositionActivePosition();

            if (isSamePosition) {
                _panel.CameraJumpButton.AddToClassList("speed-button--highlighted"); // doesnt work why
                _panel.CameraJumpButton.style.color = Color.green;
            } else {
                _panel.CameraJumpButton.RemoveFromClassList("speed-button--highlighted"); // doesnt work why
                _panel.CameraJumpButton.style.color = Color.white;
            }

            _panel.CameraJumpButton.SetEnabled(_core.ActivePosition != null && !isSamePosition && !_isFreezed);
        }
    }
}