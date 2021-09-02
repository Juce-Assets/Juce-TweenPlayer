using Juce.TweenPlayer.ReflectionComponents;
using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class ReflectionComponentFloatBinding : Binding
    {
        [SerializeField] public ReflectionComponentFloat FallbackValue 
            = new ReflectionComponentFloat();

        private ReflectionComponentFloat bindedValue;

        public override Type BindingType => typeof(ReflectionComponentFloat);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentFloat GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
