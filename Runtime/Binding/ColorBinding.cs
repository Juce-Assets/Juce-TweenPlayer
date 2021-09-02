using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class ColorBinding : Binding
    {
        [SerializeField] public Color FallbackValue = Color.white;

        private Color bindedValue;

        public override Type BindingType => typeof(Color);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Color GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
