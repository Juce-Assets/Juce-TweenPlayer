using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;
using UnityEngine.Events;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("UnityEvent Trigger", "UnityEvent/Trigger")]
    [TweenPlayerComponentColor(0.976f, 0.760f, 0.168f)]
    [TweenPlayerComponentDocumentation("Triggers a UnityEvent.")]
    [System.Serializable]
    public class UnityEventTriggerComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private UnityEventBinding target = new UnityEventBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        public override void Validate(ValidationBuilder validationBuilder)
        {
            if (!target.WantsToBeBinded && target.GetValue() == null)
            {
                validationBuilder.LogError($"Target value is null");
                validationBuilder.SetError();
            }
        }

        public override string GenerateTitle()
        {
            return target.ToString();
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            UnityEvent targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(() =>
            {
                if(targetValue == null)
                {
                    return;
                }

                targetValue.Invoke();
            });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
