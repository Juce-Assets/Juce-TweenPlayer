using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Transform Local Rotation Z", "Transform/Local Rotation X Y Z/Z")]
    [TweenPlayerComponentColor(1f, 0.368f, 0.066f)]
    [TweenPlayerComponentDocumentation("Animates the local rotation Z of a Transform.")]
    [System.Serializable]
    public class TransformLocalRotationZComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private TransformBinding target = new TransformBinding();
        [SerializeField] private FloatBinding value = new FloatBinding();
        [SerializeField] private RotationModeBinding mode = new RotationModeBinding();
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
            Transform targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            float valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            RotationMode modeValue = mode.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = targetValue.TweenLocalRotationZ(
                valueValue,
                durationValue,
                modeValue
                );

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
