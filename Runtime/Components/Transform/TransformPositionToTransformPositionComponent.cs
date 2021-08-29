using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Transform Position To Transform Position", "Transform/Position To Transform Position")]
    [TweenPlayerComponentColor(1f, 0.368f, 0.066f)]
    [System.Serializable]
    public class TransformPositionToTransformPositionComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private TransformBinding target = new TransformBinding();
        [SerializeField] private TransformBinding value = new TransformBinding();
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

            if (!value.WantsToBeBinded && value.GetValue() == null)
            {
                validationBuilder.LogError($"Value is null");
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

            ITween progressTween = target.GetValue().TweenPosition(value.GetValue().position, duration.GetValue());

            progressTween.SetEase(easing.GetValue());

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
