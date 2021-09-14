using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;
using UnityEngine.UI;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("RawImage Texture", "RawImage/Texture")]
    [TweenPlayerComponentColor(0.364f, 0.505f, 0.505f)]
    [TweenPlayerComponentDocumentation("Changes the Texture of a RawImage.")]
    [System.Serializable]
    public class RawImageTextureComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private RawImageBinding target = new RawImageBinding();
        [SerializeField] private TextureBinding value = new TextureBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private Texture lastTextureState;

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
            RawImage targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            Texture valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    lastTextureState = targetValue.texture;

                    targetValue.texture = valueValue;
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.texture = lastTextureState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
