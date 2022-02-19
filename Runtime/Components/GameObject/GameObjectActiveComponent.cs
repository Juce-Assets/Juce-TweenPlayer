using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("GameObject Active", "GameObject/Active")]
    [TweenPlayerComponentColor(0.85f, 0.89f, 0.85f)]
    [TweenPlayerComponentDocumentation("Activates/Deactivates the GameObject, " +
        "depending on the given true or false value.")]
    [System.Serializable]
    public class GameObjectActiveComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private GameObjectBinding target = new GameObjectBinding();
        [SerializeField] private BoolBinding value = new BoolBinding();
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
            GameObject targetValue = target.GetValue();

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

                    lastActiveState = targetValue.activeSelf;

                    targetValue.SetActive(valueValue);
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.SetActive(lastActiveState);
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
