using Juce.Tweening;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Utils;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [TweenPlayerComponent("UI Interactable", "UI/Interactable")]
    [TweenPlayerComponentColor(0.976f, 0.760f, 0.168f)]
    [System.Serializable]
    public class UIInteractableComponent : AnimationTweenPlayerComponent
    {
        [SerializeField] private GameObjectBinding target = new GameObjectBinding();
        [SerializeField] private BoolBinding interactable = new BoolBinding();
        [SerializeField] private BoolBinding blocksRaycast = new BoolBinding();
        [SerializeField] private FloatBinding delay = new FloatBinding();

        private bool lastInteractableState;
        private bool lastBlocksRaycastState;

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
            GameObject targetValue = target.GetValue();

            if (targetValue == null)
            {
                return ComponentExecutionResult.Empty;
            }

            CanvasGroup canvasGroup = targetValue.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = targetValue.AddComponent<CanvasGroup>();
            }

            ITween delayTween = DelayUtils.Apply(sequenceTween, delay);

            sequenceTween.AppendResetableCallback(
                () =>
                {
                    lastInteractableState = canvasGroup.interactable;
                    lastBlocksRaycastState = canvasGroup.blocksRaycasts;

                    canvasGroup.interactable = interactable.GetValue();
                    canvasGroup.blocksRaycasts = blocksRaycast.GetValue();
                },
                () =>
                {
                    canvasGroup.interactable = lastInteractableState;
                    canvasGroup.blocksRaycasts = lastBlocksRaycastState;
                });

            return new ComponentExecutionResult(delayTween);
        }
    }
}
