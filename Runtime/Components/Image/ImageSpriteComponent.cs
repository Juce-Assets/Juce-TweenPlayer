using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("Image Sprite", "Image/Sprite")]
    [TweenPlayerComponentColor(0.964f, 0.505f, 0.505f)]
    [TweenPlayerComponentDocumentation("Changes the Sprite of an Image.")]
    [System.Serializable]
    public class ImageSpriteComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ImageBinding target = new ImageBinding();
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
            Image targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            Sprite valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if(targetValue == null)
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
