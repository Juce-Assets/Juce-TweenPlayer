using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("ParticleSystem Play", "ParticleSystem/Play")]
    [TweenPlayerComponentColor(0.658f, 0.517f, 0.952f)]
    [System.Serializable]
    public class ParticleSystemPlayComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ParticleSystemBinding target = new ParticleSystemBinding();
        [SerializeField] private BoolBinding withChildren = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private bool lastActiveState;

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
            bool withChildrenValue = withChildren.GetValue();

            if (particleSystemValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    particleSystemValue.Play(withChildrenValue);
                },
                () =>
                {
                    particleSystemValue.Stop(withChildrenValue);
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
