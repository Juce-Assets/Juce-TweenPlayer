using Juce.TweenPlayer.Style;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class GeneralProgressBarDrawer 
    {
        public static void Draw(TweenPlayerEditor bindingPlayerEditor)
        {
            float progress = bindingPlayerEditor.ActualTarget.GetNormalizedProgress();

            if (progress > 0 && progress < 1)
            {
                ProgressBarDrawer.Draw(
                    bindingPlayerEditor.ActualTarget.GetNormalizedProgress(),
                    TweenPlayerEditorStyles.TaskRunningColor,
                    offsetX: -15, offsetY: -5, height: 3
                    );

                return;
            }

            if (progress >= 1)
            {
                ProgressBarDrawer.Draw(
                    bindingPlayerEditor.ActualTarget.GetNormalizedProgress(),
                    TweenPlayerEditorStyles.TaskFinishedColor,
                    offsetX: -15, offsetY: -5, height: 3
                    );
            }
        }
    }
}
