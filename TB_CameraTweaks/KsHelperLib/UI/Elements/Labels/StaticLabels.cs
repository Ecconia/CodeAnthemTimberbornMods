using System;
using System.Collections.Generic;
using System.Text;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.KsHelperLib.UI.Elements.Labels
{
    internal class StaticLabels
    {
        internal static void FooterNote(VisualElementBuilder obj, string text, int margin = -10)
        {
            obj.AddPreset(factory => factory.Labels().GameTextSmall(text: text, builder: builder => builder.SetStyle(style =>
            {
                style.alignSelf = Align.Center;
                style.fontSize = 12;
                style.paddingBottom = 15;
                style.marginTop = margin;
            })));
        }

        internal static void Spacer(VisualElementBuilder obj, int margin = -10)
        {
            obj.AddPreset(factory => factory.Labels().GameTextSmall(text: " ", builder: builder => builder.SetStyle(style =>
            {
                style.alignSelf = Align.Center;
                style.fontSize = 12;
                style.paddingBottom = 15;
                style.marginTop = margin;
            })));
        }
    }
}