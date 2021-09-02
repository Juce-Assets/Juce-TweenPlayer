using Juce.Tweening;

namespace Juce.TweenPlayer.Components
{
    [System.Serializable]
    public class ComponentExecutionResult
    {
        public static ComponentExecutionResult Empty => null;

        public ITween DelayTween { get; }
        public ITween ProgressTween { get; }

        public ComponentExecutionResult(ITween delayTween, ITween progressTween)
        {
            DelayTween = delayTween;
            ProgressTween = progressTween;
        }

        public ComponentExecutionResult(ITween delayTween)
        {
            DelayTween = delayTween;
        }

    }
}
