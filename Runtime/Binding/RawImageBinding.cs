using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;
using UnityEngine.UI;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class RawImageBinding : Binding
    {
        [SerializeField] public RawImage FallbackValue;

        private RawImage bindedValue;

        public override Type BindingType => typeof(RawImage);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public RawImage GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
