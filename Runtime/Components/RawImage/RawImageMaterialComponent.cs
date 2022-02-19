using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("RawImage Material", "RawImage/Material")]
    [TweenPlayerComponentColor(0.364f, 0.505f, 0.505f)]
    [TweenPlayerComponentDocumentation("Changes the Material of a RawImage.")]
    [System.Serializable]
    public class RawImageMaterialComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private RawImageBinding target = new RawImageBinding();
        [SerializeField] private MaterialBinding value = new MaterialBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private Material lastMaterialState;

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
            RawImage targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            Material valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    lastMaterialState = targetValue.material;

                    targetValue.material = valueValue;
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.material = lastMaterialState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
