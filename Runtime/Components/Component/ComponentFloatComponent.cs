using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.ReflectionComponents;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using System.Reflection;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("Component Float", "Component/Float")]
    [TweenPlayerComponentColor(1f, 0.160f, 0.160f)]
    [TweenPlayerComponentDocumentation("Animates a float property from another component.")]
    [System.Serializable]
    public class ComponentFloatComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ReflectionComponentFloatBinding target = new ReflectionComponentFloatBinding();
        [SerializeField] private FloatBinding value = new FloatBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();
        [SerializeField] private FloatBinding duration = new FloatBinding();
        [SerializeField] private AnimationCurveBinding easing = new AnimationCurveBinding();

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
            ReflectionComponentFloat targetValue = target.GetValue();

            if (targetValue.Component == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool found = ReflectionComponentUtils.TryFindFieldOrProperty(
                targetValue.Component.GetType(),
                targetValue.PropertyName,
                typeof(float),
                out FieldInfo fieldInfo,
                out PropertyInfo propertyInfo
                );

            if (!found)
            {
                return ComponentExecutionResult.Empty;
            }

            float valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = Tween.To(
                () => ReflectionComponentUtils.GetValue<float>(fieldInfo, propertyInfo, targetValue.Component),
                current => ReflectionComponentUtils.SetValue(fieldInfo, propertyInfo, targetValue.Component, current),
                () => valueValue,
                durationValue,
                () => targetValue.Component != null
                );

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
