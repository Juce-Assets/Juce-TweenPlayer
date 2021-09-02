using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using System.Reflection;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Component String", "Component/String")]
    [TweenPlayerComponentColor(1f, 0.160f, 0.160f)]
    [System.Serializable]
    public class ComponentStringComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private ReflectionComponentStringBinding target = new ReflectionComponentStringBinding();
        [SerializeField] private StringBinding value = new StringBinding();
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
                typeof(string),
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
                ReflectionComponentUtils.SetValue(
                    fieldInfo,
                    propertyInfo,
                    target.GetValue().Component,
                    value.GetValue()
                    );
            });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
