using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("ParticleSystem Wait Until Finished", "ParticleSystem/Wait Until Finished")]
    [TweenPlayerComponentColor(0.658f, 0.517f, 0.952f)]
    [TweenPlayerComponentDocumentation("Plays until the target ParticleSystem reaches it's final duration. " +
        "This does not work equally as the Wait All Above, since it does not preventing bottom components from playing.")]
    [System.Serializable]
    public class ParticleSystemWaitUntilFinishedComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ParticleSystemBinding target = new ParticleSystemBinding();
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
            ParticleSystem targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = Tween.To(
              () => targetValue.time,
              current => { },
              () => targetValue.main.duration,
              targetValue.main.duration,
              () => targetValue != null
              );

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
