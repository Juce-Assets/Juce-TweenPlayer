using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("GameObject Name", "GameObject/Name")]
    [TweenPlayerComponentColor(0.85f, 0.89f, 0.85f)]
    [TweenPlayerComponentDocumentation("Sets the name of the GameObject.")]
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
            GameObject targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            string valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    if(targetValue == null)
                    {
                        return;
                    }

                    lastNameState = targetValue.name;

                    targetValue.name = valueValue;
                },
                () =>
                {
                    if (targetValue == null)
                    {
                        return;
                    }

                    targetValue.name = lastNameState;
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
