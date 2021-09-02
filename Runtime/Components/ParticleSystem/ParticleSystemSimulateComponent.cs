using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("ParticleSystem Simulate", "ParticleSystem/Simulate")]
    [TweenPlayerComponentColor(0.658f, 0.517f, 0.952f)]
    [System.Serializable]
    public class ParticleSystemSimulateComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ParticleSystemBinding target = new ParticleSystemBinding();
        [SerializeField] private FloatBinding simulatedTime = new FloatBinding();
        [SerializeField] private BoolBinding withChildren = new BoolBinding();
        [SerializeField] private BoolBinding restart = new BoolBinding();
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
            ParticleSystem particleSystemValue = target.GetValue();

            if (particleSystemValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            float simulatedTimeValue = simulatedTime.GetValue();
            bool withChildrenValue = withChildren.GetValue();
            bool restartValue = restart.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(
                () =>
                {
                    particleSystemValue.Simulate(simulatedTimeValue, withChildrenValue, restartValue);
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
