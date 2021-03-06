using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.BindableData;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("TweenPlayer Conditional Play", "TweenPlayer/Conditional Play")]
    [TweenPlayerComponentColor(0.909f, 0.231f, 0.231f)]
    [TweenPlayerComponentDocumentation("Merges another TweenPlayer to the current one, and Plays it " +
        "if the condition equals true.")]
    [System.Serializable]
    public class TweenPlayerConditionalPlayComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private BoolBinding condition = new BoolBinding();
        [SerializeField] private TweenPlayerBinding targetTrue = new TweenPlayerBinding();
        [SerializeField] private BoolBinding bind = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        public override void Validate(ValidationBuilder validationBuilder)
        {
            if (!targetTrue.WantsToBeBinded && targetTrue.GetValue() == null)
            {
                validationBuilder.LogError($"Target True value is null");
                validationBuilder.SetError();
            }
        }

        public override string GenerateTitle()
        {
            return targetTrue.ToString();
        }

        public override void OnBind(IBindableData bindableData)
        {
            bool conditionValue = condition.GetValue();
            TweenPlayer targetTrueValue = targetTrue.GetValue();

            bool bindValue = bind.GetValue();

            if (targetTrueValue == null)
            {
                return;
            }

            if (!bindValue)
            {
                return;
            }

            if (conditionValue)
            {
                targetTrueValue.Bind(bindableData);
            }
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            bool conditionValue = condition.GetValue();
            TweenPlayer targetTrueValue = targetTrue.GetValue();

            if (targetTrueValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = null;

            if (conditionValue)
            {
                progressTween = targetTrueValue.GenerateSequence();
            }

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(delayTween, progressTween);
        }
    }
}
