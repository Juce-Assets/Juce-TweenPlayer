using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("RectTransform Size With Current Anchors", "RectTransform/Size With Current Anchors")]
    [TweenPlayerComponentColor(0.19f, 0.81f, 0.34f)]
    [TweenPlayerComponentDocumentation("Makes the RectTransform calculated rect be a given size on " +
        "the vertical and horizontal axis. " +
        "This produces the given size with the current anchoring. " +
        "If the parent RectTransform changes size, the size of the rect may change.")]
    [System.Serializable]
    public class RectTransformSizeWithCurrentAnchorsComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private RectTransformBinding target = new RectTransformBinding();
        [SerializeField] private Vector2Binding value = new Vector2Binding();
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
            RectTransform targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            Vector2 valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = targetValue.TweenSizeWithCurrentAnchors(valueValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
