using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("Image Fill Ammount", "Image/Fill Ammount")]
    [TweenPlayerComponentColor(0.964f, 0.505f, 0.505f)]
    [TweenPlayerComponentDocumentation("Amount of the Image shown when the Image.type is set to Image.Type.Filled. " +
        "0-1 range with 0 being nothing shown, and 1 being the full Image.")]
    [System.Serializable]
    public class ImageFillAmmountComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ImageBinding target = new ImageBinding();
        [SerializeField] private UnitFloatBinding value = new UnitFloatBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();
        [SerializeField] private FloatBinding duration = new FloatBinding();
        [SerializeField] private AnimationCurveBinding easing = new AnimationCurveBinding();

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
            Image targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            float valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = targetValue.TweenFillAmount(valueValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
