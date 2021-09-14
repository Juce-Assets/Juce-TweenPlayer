using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("UI Alpha", "UI/Alpha")]
    [TweenPlayerComponentColor(0.976f, 0.760f, 0.168f)]
    [TweenPlayerComponentDocumentation("Animates the alpha value of a CanvasGroup. " +
        "If the CanvasGroup does not exist, it's automatically created on the target GameObject")]
    [System.Serializable]
    public class UIAlphaComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private GameObjectBinding target = new GameObjectBinding();
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
            GameObject targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            CanvasGroup canvasGroup = targetValue.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = targetValue.AddComponent<CanvasGroup>();
            }

            float valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = canvasGroup.TweenAlpha(valueValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(progressTween, delayTween);
        }
    }
}
