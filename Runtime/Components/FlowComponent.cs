using Juce.Tweening;
using Juce.TweenComponent.Flow;

namespace Juce.TweenComponent.Components
{
    [System.Serializable]
    public class FlowComponent : TweenPlayerComponent
    {
        public sealed override void Execute(FlowContext context, ISequenceTween sequenceTween)
        {
            ExecutionResult = OnExecute(context);
        }

        protected virtual ComponentExecutionResult OnExecute(FlowContext context) 
        { 
            return ComponentExecutionResult.Empty; 
        }
    }
}
