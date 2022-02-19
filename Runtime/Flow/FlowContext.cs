using Juce.Tweening;

namespace Juce.TweenComponent.Flow
{
    public class FlowContext
    {
        public ISequenceTween MainSequence { get; } = JuceTween.Sequence();
        public ISequenceTween CurrentSequence { get; set; } = JuceTween.Sequence();
    }
}