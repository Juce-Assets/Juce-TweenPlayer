using Juce.TweenComponent.Components;
using Juce.TweenComponent.Style;

namespace Juce.TweenComponent.Drawers
{
    public static class ComponentProgressBarDrawer
    {
        public static void Draw(
            TweenPlayerEditor editor,
            TweenPlayerComponent component
            )
        {
            if(!editor.SerializedPropertiesData.ShowProgressBarsProperty.boolValue)
            {
                return;
            }

            if (component.ExecutionResult == ComponentExecutionResult.Empty)
            {
                return;
            }

            float offsetX = 0;
            float offsetY = 0;

            if (component.ExecutionResult.HasDelayTween)
            {
                if (component.ExecutionResult.DelayTween.IsPlaying)
                {
                    float normalizedProgress = component.ExecutionResult.DelayTween.GetNormalizedProgress();

                    ProgressBarDrawer.Draw(
                        normalizedProgress,
                        TweenPlayerEditorStyles.TaskDelayColor,
                        offsetX, offsetY
                        );

                    return;
                }

                if (!component.ExecutionResult.HasProgressTween &&
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

            if (component.ExecutionResult.HasProgressTween)
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
