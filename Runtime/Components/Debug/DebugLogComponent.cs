using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("Debug Log", "Debug/Log")]
    [TweenPlayerComponentColor(0.5f, 0.192f, 0.721f, useAsBackground: true)]
    [System.Serializable]
    public class DebugLogComponent : AnimationTweenPlayerComponent
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
                    UnityEngine.Debug.Log(logValue);
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
