using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("ParticleSystem Stop", "ParticleSystem/Stop")]
    [TweenPlayerComponentColor(0.658f, 0.517f, 0.952f)]
    [System.Serializable]
    public class ParticleSystemStopComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ParticleSystemBinding target = new ParticleSystemBinding();
        [SerializeField] private BoolBinding withChildren = new BoolBinding();
        [SerializeField] private ParticleSystemStopBehaviorBinding stopBehavior = new ParticleSystemStopBehaviorBinding();
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

            bool withChildrenValue = withChildren.GetValue();
            ParticleSystemStopBehavior stopBehaviorValue = stopBehavior.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(
                () =>
                {
                    particleSystemValue.Stop(withChildrenValue, stopBehaviorValue);
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
