using Juce.Tweening;
using Juce.TweenPlayer.Bindings;

namespace Juce.TweenPlayer.Utils
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

            ITween delayTween = new WaitTimeTween(delay.GetValue());
            sequenceTween.Append(delayTween);

            return delayTween;
        }
    }
}
