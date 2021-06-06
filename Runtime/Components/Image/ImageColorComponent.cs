using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Image Color", "Image/Color")]
    [System.Serializable]
    public class ImageColorComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ImageBinding target = new ImageBinding();
        [SerializeField] private ColorBinding value = new ColorBinding();
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
            Image imageValue = target.GetValue();
            Color colorValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            if (imageValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = imageValue.TweenColor(colorValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween);
        }
    }
}
