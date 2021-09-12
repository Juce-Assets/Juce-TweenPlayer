using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("MonoBehaviour Enabled", "MonoBehaviour/Enabled")]
    [TweenPlayerComponentColor(0.85f, 0.89f, 0.85f)]
    [TweenPlayerComponentDocumentation("Enables/Disables the MonoBehaviour, " +
        "depending on the given true or false value.")]
    [System.Serializable]
    public class MonoBehaviourActiveComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private MonoBehaviourBinding target = new MonoBehaviourBinding();
        [SerializeField] private BoolBinding value = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private bool lastEnabledState;

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
            MonoBehaviour targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    lastEnabledState = targetValue.enabled;

                    targetValue.enabled = valueValue;
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.enabled = lastEnabledState;
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
