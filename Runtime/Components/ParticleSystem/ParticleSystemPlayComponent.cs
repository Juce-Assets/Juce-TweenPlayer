using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("ParticleSystem Play", "ParticleSystem/Play")]
    [TweenPlayerComponentColor(0.658f, 0.517f, 0.952f)]
    [TweenPlayerComponentDocumentation("Starts the Particle System. Sets the Particle Systems into " +
        "play mode and enables emitting (if it has been disabled).")]
    [System.Serializable]
    public class ParticleSystemPlayComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ParticleSystemBinding target = new ParticleSystemBinding();
        [SerializeField] private BoolBinding withChildren = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private bool lastPlayingState;

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

            bool withChildrenValue = withChildren.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if(targetValue == null)
                    {
                        return;
                    }

                    lastPlayingState = targetValue.isPlaying;

                    targetValue.Play(withChildrenValue);
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    if (lastPlayingState)
                    {
                        targetValue.Play(withChildrenValue);
                    }
                    else
                    {
                        targetValue.Stop(withChildrenValue, ParticleSystemStopBehavior.StopEmittingAndClear);
                    }
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
