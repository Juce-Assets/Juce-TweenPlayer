using Juce.Tweening;
using Juce.TweenComponent.Bindings;

namespace Juce.TweenComponent.Utils
{
    public static class DelayUtils
    {
        public static ITween Apply(ISequenceTween sequenceTween, FloatBinding delay)
        {
            float delayValue = delay.GetValue();

            if(delayValue <= 0)
            {
                return null;
            }

            ITween delayTween = new WaitTimeTween(delayValue);
            sequenceTween.Append(delayTween);

            return delayTween;
        }
    }
}
