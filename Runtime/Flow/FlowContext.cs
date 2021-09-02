using Juce.Tweening;

namespace Juce.TweenPlayer.Flow
{
    public class FlowContext
    {
        public ISequenceTween MainSequence { get; } = JuceTween.Sequence();
        public ISequenceTween CurrentSequence { get; set; } = JuceTween.Sequence();
    }
}