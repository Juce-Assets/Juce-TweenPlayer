using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("AudioSource PlayOneShot", "AudioSource/Play One Shot")]
    [TweenPlayerComponentColor(0.988f, 0.752f, 0.027f)]
    [TweenPlayerComponentDocumentation("Plays an AudioClip from an AudioSource and scales the " +
        "AudioSource volume by volumeScale. " +
        "AudioSource PlayOneShot does not cancel clips that are already being played.")]
    [System.Serializable]
    public class AudioSourcePlayOneShotComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private AudioSourceBinding target = new AudioSourceBinding();
        [SerializeField] private AudioClipBinding value = new AudioClipBinding();
        [SerializeField] private UnitFloatBinding volumeScale = new UnitFloatBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();
        [SerializeField] private BoolBinding callIfCompletingInstantly = new BoolBinding();

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
            float volumeScaleValue = volumeScale.GetValue();
            bool callIfCompletingInstantlyValue = callIfCompletingInstantly.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    if (!targetValue.isActiveAndEnabled)
                    {
                        return;
                    }

                    targetValue.PlayOneShot(valueValue, volumeScaleValue);
                },
                callIfCompletingInstantlyValue
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
