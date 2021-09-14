using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.ReflectionComponents;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using System.Reflection;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Component Bool", "Component/Bool")] 
    [TweenPlayerComponentColor(1f, 0.160f, 0.160f)]
    [TweenPlayerComponentDocumentation("Sets a bool property from another component.")]
    [System.Serializable]
    public class ComponentBoolComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ReflectionComponentBoolBinding target = new ReflectionComponentBoolBinding();
        [SerializeField] private BoolBinding value = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        public override void Validate(ValidationBuilder validationBuilder)
        {
            if (!target.WantsToBeBinded && target.GetValue().Component == null)
            {
                validationBuilder.LogError($"Target value is null");
                validationBuilder.SetError();
            }
        }

        public override string GenerateTitle()
        {
            if (target.WantsToBeBinded && !target.Binded)
            {
                return string.Empty;
            }

            return target.GetValue().ToString();
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            ReflectionComponentBool targetValue = target.GetValue();

            if (targetValue.Component == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool found = ReflectionComponentUtils.TryFindFieldOrProperty(
                targetValue.Component.GetType(),
                targetValue.PropertyName,
                typeof(bool),
                out FieldInfo fieldInfo,
                out PropertyInfo propertyInfo
                );

            if (!found)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(() =>
            {
                if(targetValue.Component == null)
                {
                    return;
                }

                ReflectionComponentUtils.SetValue(
                    fieldInfo,
                    propertyInfo,
                    targetValue.Component,
                    value.GetValue()
                    );
            });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
