using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Material Renderer Color", "Material Renderer/Color")]
    [TweenPlayerComponentColor(0.835f, 0.878f, 0.294f)]
    [System.Serializable]
    public class MaterialRendererColor : AnimationTweenPlayerComponent
    {
        [SerializeField] private RendererBinding target = new RendererBinding();
        [SerializeField] private UIntBinding materialIndex = new UIntBinding();
        [SerializeField] private StringBinding materialProperty = new StringBinding();
        [SerializeField] private ColorBinding color = new ColorBinding();
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

            if (rendererValue.sharedMaterials.Length <= materialIndexValue)
            {
                validationBuilder.LogError($"Material index does not exist");
                validationBuilder.SetError();
                return;
            }

            Material material = rendererValue.sharedMaterials[materialIndexValue];

            if(material == null)
            {
                validationBuilder.LogError($"Null material");
                validationBuilder.SetError();
                return;
            }

            bool propertyExists = material.HasProperty(materialPropertyValue);

            if (!propertyExists)
            {
                validationBuilder.LogError($"Property {materialPropertyValue} does not exist");
                return;
            }
        }

        public override string GenerateTitle()
        {
            return target.ToString();
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            Renderer rendererValue = target.GetValue();
            int materialIndexValue = materialIndex.GetValue();
            string materialPropertyValue = materialProperty.GetValue();
            Color colorValue = color.GetValue();
            float delayValue = delay.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            if (rendererValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            if(rendererValue.materials.Length <= materialIndexValue)
            {
                return ComponentExecutionResult.Empty;
            }

            Material material = rendererValue.materials[materialIndexValue];

            if(material == null)
            {
                return ComponentExecutionResult.Empty;
            }

            bool propertyExists = material.HasProperty(materialPropertyValue);

            if(!propertyExists)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = material.TweenColor(colorValue, durationValue);

            progressTween.SetEase(easing.GetValue());

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
