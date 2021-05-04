using Juce.Tween;

namespace Juce.TweenPlayer.Flow
{
    public class FlowContext
    {
        //private readonly Action<string> triggerEvent;

        //public FlowContext(Action<string> triggerEvent)
        //{
        //    this.triggerEvent = triggerEvent;
        //}

        public ISequenceTween MainSequence { get; } = JuceTween.Sequence();
        public ISequenceTween CurrentSequence { get; set; } = JuceTween.Sequence();

        //public void TriggerEvent(string eventTrigger)
        //{
        //    triggerEvent?.Invoke(eventTrigger);
        //}
    }
}