using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.ReflectionComponents;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using System.Reflection;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Component Vector3", "Component/Vector3")]
    [TweenPlayerComponentColor(1f, 0.160f, 0.160f)]
    [TweenPlayerComponentDocumentation("Animates a Vector3 property from another component.")]
    [System.Serializable]
    public class ComponentVector3Component : AnimationTweenPlayerComponent
    {
        [SerializeField] private ReflectionComponentVector3Binding target = new ReflectionComponentVector3Binding();
        [SerializeField] private Vector3Binding value = new Vector3Binding();
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
            ReflectionComponentVector3 targetValue = target.GetValue();

            if (targetValue.Component == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool found = ReflectionComponentUtils.TryFindFieldOrProperty(
                targetValue.Component.GetType(),
                targetValue.PropertyName,
                typeof(Vector3),
                out FieldInfo fieldInfo,
                out PropertyInfo propertyInfo
                );

            if (!found)
            {
                return ComponentExecutionResult.Empty;
            }

            Vector3 valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = Tween.To(
                () => ReflectionComponentUtils.GetValue<Vector3>(fieldInfo, propertyInfo, targetValue.Component),
                x => ReflectionComponentUtils.SetValue(fieldInfo, propertyInfo, targetValue.Component, x),
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
