using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("SpriteRenderer Sprite", "SpriteRenderer/Sprite")]
    [TweenPlayerComponentColor(0.588f, 0.780f, 0.301f)]
    [TweenPlayerComponentDocumentation("Changes the Sprite of a SpriteRenderer.")]
    [System.Serializable]
    public class SpriteRendererSpriteComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private SpriteRendererBinding target = new SpriteRendererBinding();
        [SerializeField] private SpriteBinding value = new SpriteBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private Sprite lastSpriteState;

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

            Sprite valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    lastSpriteState = targetValue.sprite;

                    targetValue.sprite = valueValue;
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.sprite = lastSpriteState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
