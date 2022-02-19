using Juce.Tweening;
using Juce.TweenComponent.Flow;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [System.Serializable]
    public class AnimationTweenPlayerComponent : TweenPlayerComponent
    {
        public sealed override void Execute(FlowContext context, ISequenceTween sequenceTween)
        {
            ExecutionResult = OnExecute(sequenceTween);
        }

        protected virtual ComponentExecutionResult OnExecute(ISequenceTween sequenceTween) 
        {
            return ComponentExecutionResult.Empty;
        }
    }
}
