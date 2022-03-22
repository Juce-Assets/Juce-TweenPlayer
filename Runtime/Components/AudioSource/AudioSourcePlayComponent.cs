using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("AudioSource Play", "AudioSource/Play")]
    [TweenPlayerComponentColor(0.988f, 0.752f, 0.027f)]
    [TweenPlayerComponentDocumentation("Plays the AudioSource's current clip.")]
    [System.Serializable]
    public class AudioSourcePlayComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private AudioSourceBinding target = new AudioSourceBinding();
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

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    if(!targetValue.isActiveAndEnabled)
                    {
                        return;
                    }

                    targetValue.Play();
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
