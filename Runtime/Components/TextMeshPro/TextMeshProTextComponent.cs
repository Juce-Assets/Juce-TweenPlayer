﻿using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("TextMeshPro Text", "TextMeshPro/Text")]
    [System.Serializable]
    public class TextMeshProTextComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private TextMeshProBinding target = new TextMeshProBinding();
        [SerializeField] private StringBinding value = new StringBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private string lastTextState;

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
            if (target.GetValue() == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    lastTextState = target.GetValue().text;

                    target.GetValue().text = value.GetValue();
                },
                () =>
                {
                    target.GetValue().text = lastTextState;
                }
                );

            return new ComponentExecutionResult(delayTween);
        }
    }
}
