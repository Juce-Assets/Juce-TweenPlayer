using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
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
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
