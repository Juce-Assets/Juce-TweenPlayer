using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.ReflectionComponents;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using System.Reflection;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Component String", "Component/String")]
    [TweenPlayerComponentColor(1f, 0.160f, 0.160f)]
    [TweenPlayerComponentDocumentation("Sets a string property from another component.")]
    [System.Serializable]
    public class ComponentStringComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ReflectionComponentStringBinding target = new ReflectionComponentStringBinding();
        [SerializeField] private StringBinding value = new StringBinding();
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
            ReflectionComponentString targetValue = target.GetValue();

            if (targetValue.Component == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool found = ReflectionComponentUtils.TryFindFieldOrProperty(
                targetValue.Component.GetType(),
                targetValue.PropertyName,
                typeof(string),
                out FieldInfo fieldInfo,
                out PropertyInfo propertyInfo
                );

            if (!found)
            {
                return ComponentExecutionResult.Empty;
            }

            string valueValue = value.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(() =>
            {
                if (targetValue.Component == null)
                {
                    return;
                }

                ReflectionComponentUtils.SetValue(
                    fieldInfo,
                    propertyInfo,
                    targetValue.Component,
                    valueValue
                    );
            });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
