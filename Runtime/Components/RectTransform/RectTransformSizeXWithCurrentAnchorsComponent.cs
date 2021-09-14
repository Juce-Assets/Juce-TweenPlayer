using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("RectTransform Size X With Current Anchors", "RectTransform/Size With Current Anchors X Y/X")]
    [TweenPlayerComponentColor(0.19f, 0.81f, 0.34f)]
    [TweenPlayerComponentDocumentation("Makes the RectTransform calculated rect be a given size on " +
        "the horizontal axis. " +
        "This produces the given size with the current anchoring. " +
        "If the parent RectTransform changes size, the size of the rect may change.")]
    [System.Serializable]
    public class RectTransformSizeXWithCurrentAnchorsComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private RectTransformBinding target = new RectTransformBinding();
        [SerializeField] private FloatBinding value = new FloatBinding();
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

            float valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = targetValue.TweenSizeXWithCurrentAnchors(valueValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
