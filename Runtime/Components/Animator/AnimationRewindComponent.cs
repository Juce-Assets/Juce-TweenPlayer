using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Animation Rewind", "Animation/Rewind")]
    [TweenPlayerComponentColor(0.988f, 0.752f, 0.027f)]
    [TweenPlayerComponentDocumentation("Rewinds an Animation Component.")]
    [System.Serializable]
    public class AnimationRewindComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private AnimationBinding target = new AnimationBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        bool lastPlayState;

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
            Animation targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(() =>
            {
                if (targetValue == null)
                {
                    return;
                }

                lastPlayState = targetValue.isPlaying;

                targetValue.Rewind();
            },
            () =>
            {
                if (targetValue == null)
                {
                    return;
                }

                if (lastPlayState)
                {
                    targetValue.Play();
                }
                else
                {
                    targetValue.Stop();
                }
            });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
