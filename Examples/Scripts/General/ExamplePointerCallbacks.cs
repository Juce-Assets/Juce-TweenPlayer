using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JuceNew.Examples
{
    public class ExamplePointerCallbacks : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private ExamplePointerCallbackPressState pressState = ExamplePointerCallbackPressState.Up;
        private ExamplePointerCallbackPositionState positionState = ExamplePointerCallbackPositionState.Out;

        public event Action<ExamplePointerCallbacks, PointerEventData> OnEnter;
        public event Action<ExamplePointerCallbacks, PointerEventData> OnExit;
        public event Action<ExamplePointerCallbacks, PointerEventData> OnDown;
        public event Action<ExamplePointerCallbacks, PointerEventData> OnUp;
        public event Action<ExamplePointerCallbacks, PointerEventData> OnClick;

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                TrySetPressState(ExamplePointerCallbackPressState.Up, new PointerEventData(EventSystem.current));
                TrySetPositionState(ExamplePointerCallbackPositionState.Out, new PointerEventData(EventSystem.current));
            }
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            TrySetPressState(ExamplePointerCallbackPressState.Down, pointerEventData);
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            TrySetPressState(ExamplePointerCallbackPressState.Up, pointerEventData);
            TrySetPositionState(ExamplePointerCallbackPositionState.Out, pointerEventData);
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            TrySetPositionState(ExamplePointerCallbackPositionState.In, pointerEventData);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            TrySetPositionState(ExamplePointerCallbackPositionState.Out, pointerEventData);
            TrySetPressState(ExamplePointerCallbackPressState.Up, pointerEventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this, eventData);
        }

        private void TrySetPressState(ExamplePointerCallbackPressState pressState, PointerEventData pointerEventData)
        {
            switch (pressState)
            {
                case ExamplePointerCallbackPressState.Up:
                    {
                        if (this.pressState == ExamplePointerCallbackPressState.Down)
                        {
                            this.pressState = pressState;

                            OnUp?.Invoke(this, pointerEventData);
                        }
                    }
                    break;

                case ExamplePointerCallbackPressState.Down:
                    {
                        if (this.pressState == ExamplePointerCallbackPressState.Up)
                        {
                            this.pressState = pressState;

                            OnDown?.Invoke(this, pointerEventData);
                        }
                    }
                    break;
            }
        }

        private void TrySetPositionState(ExamplePointerCallbackPositionState positionState, PointerEventData pointerEventData)
        {
            switch (positionState)
            {
                case ExamplePointerCallbackPositionState.In:
                    {
                        if (this.positionState == ExamplePointerCallbackPositionState.Out)
                        {
                            this.positionState = positionState;

                            OnEnter?.Invoke(this, pointerEventData);
                        }
                    }
                    break;

                case ExamplePointerCallbackPositionState.Out:
                    {
                        if (this.positionState == ExamplePointerCallbackPositionState.In)
                        {
                            this.positionState = positionState;

                            OnExit?.Invoke(this, pointerEventData);
                        }
                    }
                    break;
            }
        }
    }
}
