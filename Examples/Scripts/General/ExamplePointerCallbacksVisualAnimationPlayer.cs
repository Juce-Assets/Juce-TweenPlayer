using Juce.TweenPlayer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JuceNew.Examples
{
    public class ExamplePointerCallbacksVisualAnimationPlayer : MonoBehaviour
    {
        [SerializeField] private ExamplePointerCallbacks pointerCallbacks = default;

        [Header("VisualAnimation")]
        [SerializeField] private TweenPlayer onPointerUp = default;
        [SerializeField] private TweenPlayer onPointerDown = default;

        private void Awake()
        {
            pointerCallbacks.OnUp += OnPointerCallbacksUp;
            pointerCallbacks.OnDown += OnPointerCallbacksDown;
        }

        private void OnDestroy()
        {
            pointerCallbacks.OnUp -= OnPointerCallbacksUp;
            pointerCallbacks.OnDown -= OnPointerCallbacksDown;
        }

        private void OnPointerCallbacksUp(ExamplePointerCallbacks pointerCallblacks, PointerEventData pointerEventData)
        {
            onPointerDown?.Kill();
            onPointerUp?.Play();
        }

        private void OnPointerCallbacksDown(ExamplePointerCallbacks pointerCallblacks, PointerEventData pointerEventData)
        {
            onPointerUp?.Kill();
            onPointerDown?.Play();
        }
    }
}
