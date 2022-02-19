using Juce.TweenComponent.ReflectionComponents;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class ReflectionComponentFloatBinding : Binding
    {
        [SerializeField] public ReflectionComponentFloat FallbackValue 
            = new ReflectionComponentFloat();

        private ReflectionComponentFloat bindedValue = new ReflectionComponentFloat();

        public override Type BindingType => typeof(ReflectionComponentFloat);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentFloat GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
