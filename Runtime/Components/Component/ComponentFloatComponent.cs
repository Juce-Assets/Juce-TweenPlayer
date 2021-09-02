using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using System.Reflection;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Component Float", "Component/Float")]
    [TweenPlayerComponentColor(1f, 0.160f, 0.160f)]
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
            if (target.GetValue().Component == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool found = ReflectionComponentUtils.TryFind(
                target.GetValue().Component.GetType(),
                target.GetValue().PropertyName,
                typeof(float),
                out FieldInfo fieldInfo,
                out PropertyInfo propertyInfo
                );

            if (!found)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = Tween.To(
                () => ReflectionComponentUtils.GetValue<float>(fieldInfo, propertyInfo, target.GetValue().Component),
                x => ReflectionComponentUtils.SetValue(fieldInfo, propertyInfo, target.GetValue().Component, x),
                () => value.GetValue(),
                duration.GetValue(),
                () => target.GetValue() != null
                );

            progressTween.SetEase(easing.GetValue());

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
