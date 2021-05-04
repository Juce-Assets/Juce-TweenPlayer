using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("GameObject Active", "GameObject/Active")]
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
            if (target.GetValue() == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    lastActiveState = target.GetValue().activeSelf;

                    target.GetValue().SetActive(value.GetValue());
                },
                () =>
                {
                    target.GetValue().SetActive(lastActiveState);
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
