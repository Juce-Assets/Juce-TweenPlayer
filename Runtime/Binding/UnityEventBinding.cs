using System;
using UnityEngine;
using Juce.TweenComponent.Utils;
using UnityEngine.Events;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class UnityEventBinding : Binding
    {
        [SerializeField] public UnityEvent FallbackValue = default;

        private UnityEvent bindedValue;

        public override Type BindingType => typeof(UnityEvent);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public UnityEvent GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
