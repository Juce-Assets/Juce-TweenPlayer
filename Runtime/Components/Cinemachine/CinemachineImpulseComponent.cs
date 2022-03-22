#if JUCE_CINEMACHINE_EXTENSIONS

using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;
using Cinemachine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("Cinemachine Impulse", "Cinemachine/Impulse")]
    [TweenPlayerComponentColor(0.1f, 0.405f, 0.805f)]
    [TweenPlayerComponentDocumentation("Changes the base color of an Image.")]
    [System.Serializable]
    public class CinemachineImpulseComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private CinemachineImpulseSourceBinding target = new CinemachineImpulseSourceBinding();
        [SerializeField] private Vector3Binding position = new Vector3Binding();
        [SerializeField] private Vector3Binding velocity = new Vector3Binding();
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
            CinemachineImpulseSource targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            Vector3 positionValue = position.GetValue();
            Vector3 velocityValue = velocity.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(() =>
            {
                if(targetValue == null)
                {
                    return;
                }

                targetValue.GenerateImpulseAt(positionValue, velocityValue);
            });

            return new ComponentExecutionResult(delayTween);
        }
    }
}

#endif
