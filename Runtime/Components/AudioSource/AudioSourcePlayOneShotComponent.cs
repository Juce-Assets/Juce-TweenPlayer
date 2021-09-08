using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("AudioSource PlayOneShot", "AudioSource/PlayOneShot")]
    [TweenPlayerComponentColor(0.988f, 0.752f, 0.027f)]
    [System.Serializable]
    public class AudioSourcePlayOneShotComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private AudioSourceBinding target = new AudioSourceBinding();
        [SerializeField] private AudioClipBinding value = new AudioClipBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

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
            AudioSource targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            AudioClip valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.PlayOneShot(valueValue);
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
