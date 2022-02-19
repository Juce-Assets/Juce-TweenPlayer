using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class RectTransformBinding : Binding
    {
        [SerializeField] public RectTransform FallbackValue;

        private RectTransform bindedValue;

        public RectTransform Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(RectTransform);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public RectTransform GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
