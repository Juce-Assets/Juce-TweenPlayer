using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.BindableData;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("TweenPlayer Play", "TweenPlayer/Play")]
    [TweenPlayerComponentColor(0.909f, 0.231f, 0.231f)]
    [TweenPlayerComponentDocumentation("Merges another TweenPlayer to the current one, and Plays it.")]
    [System.Serializable]
    public class TweenPlayerPlayComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private TweenPlayerBinding target = new TweenPlayerBinding();
        [SerializeField] private BoolBinding complete = new BoolBinding();
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

            bool completeValue = complete.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = targetValue.GenerateSequence();

            sequenceTween.Append(progressTween);

            if (completeValue)
            {
                sequenceTween.JoinCallback(progressTween.Complete);
            }

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
