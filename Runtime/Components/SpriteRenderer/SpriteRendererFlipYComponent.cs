using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("SpriteRenderer Flip Y", "SpriteRenderer/Flip Y")]
    [TweenPlayerComponentColor(0.588f, 0.780f, 0.301f)]
    [TweenPlayerComponentDocumentation("Toggles the flip Y property of a SpriteRenderer.")]
    [System.Serializable]
    public class SpriteRendererFlipYComponent : AnimationTweenPlayerComponent
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

                    targetValue.flipY = valueValue;
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.flipY = lastFlipState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
