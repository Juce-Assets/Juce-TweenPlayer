using Juce.Tweening;
using Juce.TweenPlayer.Flow;

namespace Juce.TweenPlayer.Components
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
