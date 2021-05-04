﻿using Juce.Tween;
using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Tween Player Play", "Tween Player/Play")]
    [System.Serializable]
    [MovedFrom(false, null, null, "AnimationPlayerPlayerPlayComponent")]
    public class TweenPlayerPlayComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private TweenPlayerBinding target = new TweenPlayerBinding();
        [SerializeField] private BoolBinding bind = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

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

        public override void OnBind(IBindableData bindableData)
        {
            TweenPlayer targetValue = target.GetValue();
            bool bindValue = bind.GetValue();

            if (targetValue == null)
            {
                return;
            }

            if(!bindValue)
            {
                return;
            }

            targetValue.Bind(bindableData);
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            TweenPlayer targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = target.GetValue().GenerateSequence();

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
