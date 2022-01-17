using Juce.Tweening;
using Juce.TweenPlayer.Attributes;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Material Renderer Vector", "MaterialRenderer/Vector")]
    [TweenPlayerComponentColor(0.835f, 0.878f, 0.294f)]
    [TweenPlayerComponentDocumentation("Changes a Vector material property of a Material Renderer.")]
    [System.Serializable]
    public class MaterialRendererVector : AnimationTweenPlayerComponent
    {
        [SerializeField] private RendererBinding target = new RendererBinding();
        [SerializeField] private UIntBinding materialIndex = new UIntBinding();
        [SerializeField] private StringBinding materialProperty = new StringBinding();
        [SerializeField] private Vector4Binding value = new Vector4Binding();
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
                    UnityEditor.ShaderUtil.ShaderPropertyType.Vector,
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
            Vector4 valueValue = value.GetValue();
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

            ITween progressTween = material.TweenVector(valueValue, materialPropertyValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
