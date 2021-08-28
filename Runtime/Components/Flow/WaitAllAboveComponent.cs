using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Flow;
using Juce.TweenPlayer.Utils;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Wait All Above", "Flow/WaitAllAbove")]
    [TweenPlayerComponentColor(0.35f, 0.44f, 0.88f)]
    public class WaitAllAboveComponent : FlowComponent
    {
        [SerializeField] private FloatBinding delay = new FloatBinding();

        protected override ComponentExecutionResult OnExecute(FlowContext context)
        {
            ITween delayTween = DelayUtils.Apply(context.CurrentSequence, delay);

            context.MainSequence.Append(context.CurrentSequence);

            context.CurrentSequence = JuceTween.Sequence();

            return new ComponentExecutionResult(delayTween);
        }
    }
}
