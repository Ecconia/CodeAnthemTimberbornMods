using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.UI_Interface
{
    internal class CameraPositionUI_InterfacePresetFactory
    {
        public void AddLocalizableLabel(VisualElementBuilder builder, string locKey) {
            builder.AddPreset(factory => factory.Labels()
                .GameText(locKey: locKey,
                builder: builder => builder.SetStyle(style => {
                    style.alignSelf = Align.Center;
                    style.marginLeft = new Length(3, LengthUnit.Pixel);
                    style.marginRight = new Length(3, LengthUnit.Pixel);
                    style.marginTop = new Length(3, LengthUnit.Pixel);
                    style.marginBottom = new Length(3, LengthUnit.Pixel);
                })
            ));
        }

        public void AddLocalizableButton(VisualElementBuilder builder, string locKey, string name) {
            builder.AddPreset(factory => factory.Buttons().ButtonGame(
                    locKey: locKey, name: name,
                    fontSize: new Length(9, LengthUnit.Pixel),
                    width: new Length(80, LengthUnit.Pixel), height: new Length(22, LengthUnit.Pixel),
                    builder: builder => builder.SetStyle(style => {
                        style.alignSelf = Align.Center;
                        style.marginLeft = new Length(3, LengthUnit.Pixel);
                        style.marginRight = new Length(3, LengthUnit.Pixel);
                        style.marginTop = new Length(3, LengthUnit.Pixel);
                        style.marginBottom = new Length(3, LengthUnit.Pixel);
                    })
                ));
        }

        public void AddTextButton(VisualElementBuilder builder, string text, string name) {
            builder.AddPreset(factory => factory.Buttons().ButtonGame(
                    text: text, name: name,
                    fontSize: new Length(9, LengthUnit.Pixel),
                    width: new Length(100, LengthUnit.Pixel), height: new Length(22, LengthUnit.Pixel),
                    builder: builder => builder.SetStyle(style => {
                        style.alignSelf = Align.Center;
                        style.marginLeft = new Length(3, LengthUnit.Pixel);
                        style.marginRight = new Length(3, LengthUnit.Pixel);
                        style.marginTop = new Length(3, LengthUnit.Pixel);
                        style.marginBottom = new Length(3, LengthUnit.Pixel);
                    })
                ));
        }

        public void AddSmallButton(VisualElementBuilder builder, string text, string name) {
            builder.AddPreset(factory => factory.Buttons().ButtonGame(
                    text: text, name: name,
                    fontSize: new Length(9, LengthUnit.Pixel),
                    width: new Length(30, LengthUnit.Pixel), height: new Length(22, LengthUnit.Pixel),
                    builder: builder => builder.SetStyle(style => {
                        style.alignSelf = Align.Center;
                        style.marginLeft = new Length(3, LengthUnit.Pixel);
                        style.marginRight = new Length(3, LengthUnit.Pixel);
                        style.marginTop = new Length(3, LengthUnit.Pixel);
                        style.marginBottom = new Length(3, LengthUnit.Pixel);
                    })
                ));
        }

        public void AddSpacer(VisualElementBuilder builder, int size) {
            builder.AddPreset(factory => factory.Labels()
                .GameText(
                builder: builder => builder.SetStyle(style => {
                    style.width = new Length(size, LengthUnit.Pixel);
                    style.alignSelf = Align.Center;
                    style.marginLeft = new Length(3, LengthUnit.Pixel);
                    style.marginRight = new Length(3, LengthUnit.Pixel);
                    style.marginTop = new Length(3, LengthUnit.Pixel);
                    style.marginBottom = new Length(3, LengthUnit.Pixel);
                })
            ));
        }
    }
}