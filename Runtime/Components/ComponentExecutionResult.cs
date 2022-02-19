using Juce.Tweening;

namespace Juce.TweenComponent.Components
{
    [System.Serializable]
    public class ComponentExecutionResult
    {
        public static ComponentExecutionResult Empty => null;

        public ITween DelayTween { get; }
        public ITween ProgressTween { get; }

        public bool HasDelayTween => DelayTween != null;
        public bool HasProgressTween => ProgressTween != null;

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
