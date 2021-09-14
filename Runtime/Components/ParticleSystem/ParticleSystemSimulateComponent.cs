using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("ParticleSystem Simulate", "ParticleSystem/Simulate")]
    [TweenPlayerComponentColor(0.658f, 0.517f, 0.952f)]
    [TweenPlayerComponentDocumentation("Fast-forwards the Particle System by simulating particles " +
        "over the given period of time, then pauses it.")]
    [System.Serializable]
    public class ParticleSystemSimulateComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ParticleSystemBinding target = new ParticleSystemBinding();
        [SerializeField] private FloatBinding simulatedTime = new FloatBinding();
        [SerializeField] private BoolBinding withChildren = new BoolBinding();
        [SerializeField] private BoolBinding restart = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private float lastSimulationTimeState;

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

            float simulatedTimeValue = simulatedTime.GetValue();
            bool withChildrenValue = withChildren.GetValue();
            bool restartValue = restart.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if(targetValue == null)
                    {
                        return;
                    }

                    lastSimulationTimeState = targetValue.time;

                    targetValue.Simulate(simulatedTimeValue, withChildrenValue, restartValue);
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.Simulate(lastSimulationTimeState, withChildrenValue, restart: true);
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
