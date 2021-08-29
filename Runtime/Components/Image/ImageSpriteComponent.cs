using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Image Sprite", "Image/Sprite")]
    [TweenPlayerComponentColor(0.964f, 0.505f, 0.505f)]
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
            if (target.GetValue() == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    lastSpriteState = target.GetValue().sprite;

                    target.GetValue().sprite = value.GetValue();
                },
                () =>
                {
                    target.GetValue().sprite = lastSpriteState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
