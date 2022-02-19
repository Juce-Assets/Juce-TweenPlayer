using Juce.Tweening;
using Juce.TweenComponent.Attributes;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Utils;
using UnityEngine;

namespace Juce.TweenComponent.Components
{
    [TweenPlayerComponent("Debug Log Error", "Debug/Log Error")]
    [TweenPlayerComponentColor(0.5f, 0.192f, 0.721f, useAsBackground: true)]
    [System.Serializable]
    public class DebugLogErrorComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private StringBinding log = new StringBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        public override string GenerateTitle()
        {
            return log.ToString();
        }

        protected override ComponentExecutionResult OnExecute(ISequenceTween sequenceTween)
        {
            string logValue = log.GetValue();

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendCallback(
                () =>
                {
                    UnityEngine.Debug.LogError(logValue);
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
