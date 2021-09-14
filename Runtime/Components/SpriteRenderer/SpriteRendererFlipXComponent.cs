using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("SpriteRenderer Flip X", "SpriteRenderer/Flip X")]
    [TweenPlayerComponentColor(0.588f, 0.780f, 0.301f)]
    [TweenPlayerComponentDocumentation("Toggles the flip X property of a SpriteRenderer.")]
    [System.Serializable]
    public class SpriteRendererFlipXComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private SpriteRendererBinding target = new SpriteRendererBinding();
        [SerializeField] private BoolBinding value = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private bool lastFlipState;

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
            SpriteRenderer targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    lastFlipState = targetValue.sprite;

                    targetValue.flipX = valueValue;
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.flipX = lastFlipState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
