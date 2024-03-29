﻿using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("RectTransform Anchor Max", "RectTransform/Anchor Max")]
    [TweenPlayerComponentColor(0.19f, 0.81f, 0.34f)]
    [TweenPlayerComponentDocumentation("The normalized position in the parent RectTransform that " +
        "the upper right corner is anchored to.")]
    [System.Serializable]
    public class RectTransformAnchorMaxComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private RectTransformBinding target = new RectTransformBinding();
        [SerializeField] private Vector2Binding value = new Vector2Binding();
        [SerializeField] private FloatBinding delay = new FloatBinding();
        [SerializeField] private FloatBinding duration = new FloatBinding();
        [SerializeField] private AnimationCurveBinding easing = new AnimationCurveBinding();

        public override void Validate(ValidationBuilder validationBuilder)
        {
            if (!target.WantsToBeBinded && target.GetValue() == null)
            {
                validationBuilder.LogError($"Target value is null");
                validationBuilder.SetError();
            }
        }

        public override string GenerateTitle()
        {
            return target.ToString();
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            RectTransform targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            Vector2 valueValue = value.GetValue();
            float durationValue = duration.GetValue();
            AnimationCurve easingValue = easing.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = targetValue.TweenAnchorMax(valueValue, durationValue);

            progressTween.SetEase(easingValue);

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
