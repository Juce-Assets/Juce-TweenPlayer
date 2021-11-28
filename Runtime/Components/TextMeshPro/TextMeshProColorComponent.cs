#if JUCE_TEXT_MESH_PRO_EXTENSIONS

using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("TextMeshPro Color", "TextMeshPro/Color")]
    [TweenPlayerComponentColor(0.5f, 0.468f, 0.266f)]
    [TweenPlayerComponentDocumentation("Sets the TextMeshPro color.")]
    [System.Serializable]
    public class TextMeshProColorComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private TextMeshProBinding target = new TextMeshProBinding();
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
            TMPro.TextMeshProUGUI targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            Color valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = targetValue.TweenColor(valueValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}

#endif
