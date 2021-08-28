using Juce.Tween;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("UI Interactable", "UI/Interactable")]
    [TweenPlayerComponentColor(0.77f, 0.74f, 0.16f)]
    [System.Serializable]
    public class UIInteractableComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private GameObjectBinding target = new GameObjectBinding();
        [SerializeField] private BoolBinding interactable = new BoolBinding();
        [SerializeField] private BoolBinding blocksRaycast = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private bool lastInteractable;
        private bool lastBlocksRaycast;

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

            CanvasGroup canvasGroup = target.GetValue().GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = target.GetValue().AddComponent<CanvasGroup>();
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            ITween progressTween = new ResetableCallbackTween(
                () =>
                {
                    lastInteractable = canvasGroup.interactable;
                    lastBlocksRaycast = canvasGroup.blocksRaycasts;

                    canvasGroup.interactable = interactable.GetValue();
                    canvasGroup.blocksRaycasts = blocksRaycast.GetValue();
                },
                () =>
                {
                    canvasGroup.interactable = lastInteractable;
                    canvasGroup.blocksRaycasts = lastBlocksRaycast;
                }
                );

            sequenceTween.Append(progressTween);

            return new ComponentExecutionResult(progressTween, delayTween);
        }
    }
}
