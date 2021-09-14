using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Material Renderer Color", "Material Renderer/Color")]
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
            Renderer rendererValue = target.GetValue();
            int materialIndexValue = materialIndex.GetValue();
            string materialPropertyValue = materialProperty.GetValue();

            if (!target.WantsToBeBinded && rendererValue == null)
            {
                validationBuilder.LogError($"Target value is null");
                validationBuilder.SetError();
                return;
            }

#if UNITY_EDITOR

            MaterialRendererUtils.Validate(
                rendererValue,
                materialIndexValue,
                materialPropertyValue,
                UnityEditor.ShaderUtil.ShaderPropertyType.Color,
                validationBuilder
                );

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
