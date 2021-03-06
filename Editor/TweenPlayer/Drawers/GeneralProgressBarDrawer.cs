using Juce.TweenComponent.Style;
using UnityEngine;

namespace Juce.TweenComponent.Drawers
{
    public static class GeneralProgressBarDrawer 
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            if(!editor.SerializedPropertiesData.ShowProgressBarsProperty.boolValue)
            {
                return;
            }

            float progress = editor.ActualTarget.GetNormalizedProgress();

            if (progress > 0 && progress < 1)
            {
                ProgressBarDrawer.Draw(
                    editor.ActualTarget.GetNormalizedProgress(),
                    TweenPlayerEditorStyles.TaskRunningColor,
                    offsetX: -15, offsetY: -5, height: 3
                    );

                return;
            }

            if (progress >= 1)
            {
                ProgressBarDrawer.Draw(
                    editor.ActualTarget.GetNormalizedProgress(),
                    TweenPlayerEditorStyles.TaskFinishedColor,
                    offsetX: -15, offsetY: -5, height: 3
                    );
            }
        }
    }
}
