using Juce.TweenPlayer.Components;

namespace Juce.TweenPlayer.Drawers
{
    public static class ComponentProgressBarDrawer
    {
        public static void Draw(TweenPlayerComponent component)
        {
            if (component.ExecutionResult == ComponentExecutionResult.Empty)
            {
                return;
            }

            float offsetX = 0;
            float offsetY = 0;

            if (component.ExecutionResult.DelayTween != null)
            {
                if (component.ExecutionResult.DelayTween.IsPlaying)
                {
                    float normalizedProgress = component.ExecutionResult.ProgressTween.GetNormalizedProgress();

                    ProgressBarDrawer.Draw(
                        normalizedProgress,
                        TweenPlayerEditorStyles.TaskDelayColor,
                        offsetX, offsetY
                        );

                    return;
                }

                if (component.ExecutionResult.ProgressTween == null &&
                    component.ExecutionResult.DelayTween.IsCompleted)
                {
                    ProgressBarDrawer.Draw(
                        1.0f,
                        TweenPlayerEditorStyles.TaskFinishedColor,
                        offsetX,
                        offsetY
                        );

                    return;
                }
            }

            if (component.ExecutionResult.ProgressTween != null)
            {
                if (component.ExecutionResult.ProgressTween.IsPlaying)
                {
                    float normalizedProgress = component.ExecutionResult.ProgressTween.GetNormalizedProgress();

                    ProgressBarDrawer.Draw(
                        normalizedProgress,
                        TweenPlayerEditorStyles.TaskRunningColor,
                        offsetX,
                        offsetY
                        );

                    return;
                }

                if (component.ExecutionResult.ProgressTween.IsCompleted)
                {
                    ProgressBarDrawer.Draw(
                        1.0f,
                        TweenPlayerEditorStyles.TaskFinishedColor,
                        offsetX,
                        offsetY
                        );

                    return;
                }
            }
        }
    }
}
