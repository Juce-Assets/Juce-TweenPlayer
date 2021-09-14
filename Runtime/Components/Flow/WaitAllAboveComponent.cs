using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Flow;
using Juce.TweenPlayer.Utils;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Wait All Above", "Flow/WaitAllAbove")]
    [TweenPlayerComponentColor(0f, 0.792f, 0.721f, useAsBackground: true)]
    [TweenPlayerComponentDocumentation("Waits until all above TweenComponents are finished, before " +
        "playing the below ones. A delay can be set to wait for the execution of the TweenComponents " +
        "below.")]
    public class WaitAllAboveComponent : FlowComponent
    {
        [SerializeField] private FloatBinding delay = new FloatBinding();

        protected override ComponentExecutionResult OnExecute(FlowContext context)
        {
            ITween delayTween = DelayUtils.Apply(context.CurrentSequence, delay);

            context.MainSequence.Append(context.CurrentSequence);

            ITween progressTween = context.CurrentSequence;

            context.CurrentSequence = JuceTween.Sequence();

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
