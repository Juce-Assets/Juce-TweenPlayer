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
    [TweenPlayerComponent("Component Vector2", "Component/Vector2")]
    [TweenPlayerComponentColor(1f, 0.160f, 0.160f)]
    [TweenPlayerComponentDocumentation("Animates a Vector2 property from another component.")]
    [System.Serializable]
    public class ComponentVector2Component : AnimationTweenPlayerComponent
    {
        [SerializeField] private ReflectionComponentVector2Binding target = new ReflectionComponentVector2Binding();
        [SerializeField] private Vector2Binding value = new Vector2Binding();
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
            ReflectionComponentVector2 targetValue = target.GetValue();

            if (targetValue.Component == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool found = ReflectionComponentUtils.TryFindFieldOrProperty(
                targetValue.Component.GetType(),
                targetValue.PropertyName,
                typeof(Vector2),
                out FieldInfo fieldInfo,
                out PropertyInfo propertyInfo
                );

            if (!found)
            {
                return ComponentExecutionResult.Empty;
            }

            Vector2 valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = Tween.To(
                () => ReflectionComponentUtils.GetValue<Vector2>(fieldInfo, propertyInfo, targetValue.Component),
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
