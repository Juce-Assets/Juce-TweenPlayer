using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("RectTransform Anchored Position X", "RectTransform/Anchored Position X")]
    [TweenPlayerComponentColor(0.19f, 0.81f, 0.34f)]
    [TweenPlayerComponentDocumentation("The position X of the pivot of a RectTransform " +
        "relative to the anchor reference point.")]
    [System.Serializable]
    public class RectTransformAnchoredPositionXComponent : AnimationTweenPlayerComponent
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
            if (target.GetValue() == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = target.GetValue().TweenAnchoredPositionX(value.GetValue(), duration.GetValue());

            progressTween.SetEase(easing.GetValue());

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
