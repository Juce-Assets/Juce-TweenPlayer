using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("UI Alpha", "UI/Alpha")]
    [TweenPlayerComponentColor(0.77f, 0.74f, 0.16f)]
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
            if (target.GetValue() == null)
            {
                return ComponentExecutionResult.Empty;
            }

            CanvasGroup canvasGroup = target.GetValue().GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = target.GetValue().AddComponent<CanvasGroup>();
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = canvasGroup.TweenAlpha(value.GetValue(), duration.GetValue());

            progressTween.SetEase(easing.GetValue());

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(progressTween, delayTween);
        }
    }
}
