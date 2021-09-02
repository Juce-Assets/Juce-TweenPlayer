using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("GameObject Name", "GameObject/Name")]
    [TweenPlayerComponentColor(0.85f, 0.89f, 0.85f)]
    [System.Serializable]
    public class GameObjectNameComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private GameObjectBinding target = new GameObjectBinding();
        [SerializeField] private StringBinding value = new StringBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private string lastNameState;

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
                    lastNameState = target.GetValue().name;

                    target.GetValue().name = value.GetValue();
                },
                () =>
                {
                    target.GetValue().name = lastNameState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
