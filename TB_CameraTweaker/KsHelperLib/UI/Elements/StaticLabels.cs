using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.KsHelperLib.UI.Elements
{
    public static class StaticLabels
    {
        /// <summary>
        /// Creates a small footer note
        /// </summary>
        public static void FooterNote(VisualElementBuilder obj, string text, int margin = -10) {
            obj.AddPreset(factory => factory.Labels().GameTextSmall(text: text, builder: builder => builder.SetStyle(style => {
                style.alignSelf = Align.Center;
                style.fontSize = 12;
                style.paddingBottom = 15;
                style.marginTop = margin;
            })));
        }

        /// <summary>
        /// Creates a spacer
        /// </summary>
        public static void Spacer(VisualElementBuilder obj, int margin = -10) {
            obj.AddPreset(factory => factory.Labels().GameTextSmall(text: " ", builder: builder => builder.SetStyle(style => {
                style.alignSelf = Align.Center;
                style.fontSize = 12;
                style.paddingBottom = 15;
                style.marginTop = margin;
            })));
        }

        /// <summary>
        /// Creates a sub title
        /// </summary>
        public static void MenuSubTitle(VisualElementBuilder builder, string subTitle) {
            builder.AddPreset(factory => factory.Labels().DefaultBig(
                locKey: subTitle,
                builder: builder => builder.SetStyle(
                    style => {
                        style.alignSelf = Align.Center; style.marginBottom = new Length(10, LengthUnit.Pixel);
                    })));
        }
    }
}