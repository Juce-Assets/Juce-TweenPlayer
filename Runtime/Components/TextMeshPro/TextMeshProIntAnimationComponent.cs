using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("TextMeshPro Int Animation", "TextMeshPro/Int Animation")]
    [TweenPlayerComponentColor(0.5f, 0.468f, 0.266f)]
    [TweenPlayerComponentDocumentation("Animates an int value, from a start value to an end value, and sets it " +
        "to a TextMeshPro text.")]
    [System.Serializable]
    public class TextMeshProIntAnimationComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private TextMeshProBinding target = new TextMeshProBinding();
        [SerializeField] private IntBinding startValue = new IntBinding();
        [SerializeField] private IntBinding endValue = new IntBinding();
        [SerializeField] private StringBinding formating = new StringBinding();
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
            TMPro.TextMeshProUGUI targetValue = target.GetValue();
            int startValueValue = startValue.GetValue();
            int endValueValue = endValue.GetValue();
            string formatingValue = formating.GetValue();
            float delayValue = delay.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            int currentValue = startValueValue;

            ITween progressTween = Tweening.Tween.To(
                () => currentValue,
                current =>
                {
                    currentValue = current;

                    targetValue.text = string.Format(formatingValue, currentValue.ToString());
                },
                () => endValueValue,
                durationValue,
                () => targetValue != null
                );

            progressTween.SetEase(easing.GetValue());

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}

