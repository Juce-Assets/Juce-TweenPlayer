using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("Material Renderer Color", "MaterialRenderer/Color")]
    [TweenPlayerComponentColor(0.835f, 0.878f, 0.294f)]
    [TweenPlayerComponentDocumentation("Changes a Color material property of a Material Renderer.")]
    [System.Serializable]
    public class MaterialRendererColor : AnimationTweenPlayerComponent
    {
        [SerializeField] private RendererBinding target = new RendererBinding();
        [SerializeField] private UIntBinding materialIndex = new UIntBinding();
        [SerializeField] private StringBinding materialProperty = new StringBinding();
        [SerializeField] private ColorBinding value = new ColorBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();
        [SerializeField] private FloatBinding duration = new FloatBinding();
        [SerializeField] private AnimationCurveBinding easing = new AnimationCurveBinding();

        public override void Validate(ValidationBuilder validationBuilder)
        {
            if (!target.WantsToBeBinded && target.GetValue() == null)
            {
                validationBuilder.LogError($"Target value is null");
                validationBuilder.SetError();
                return;
            }

#if UNITY_EDITOR

            if (!target.WantsToBeBinded && !materialIndex.WantsToBeBinded && !materialProperty.WantsToBeBinded)
            {
                Renderer rendererValue = target.GetValue();
                int materialIndexValue = materialIndex.GetValue();
                string materialPropertyValue = materialProperty.GetValue();

                MaterialRendererUtils.Validate(
                    rendererValue,
                    materialIndexValue,
                    materialPropertyValue,
                    UnityEditor.ShaderUtil.ShaderPropertyType.Color,
                    validationBuilder
                    );
            }

#endif
        }

        public override string GenerateTitle()
        {
            return target.ToString();
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            Renderer targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            int materialIndexValue = materialIndex.GetValue();
            string materialPropertyValue = materialProperty.GetValue();
            Color valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            bool propertyExists = MaterialRendererUtils.PropertyExists(
                targetValue,
                materialIndexValue,
                materialPropertyValue
                );

            if (!propertyExists)
            {
                return ComponentExecutionResult.Empty;
            }

            Material material = targetValue.materials[materialIndexValue];

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = material.TweenColor(valueValue, materialPropertyValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
