using Juce.TweenComponent.ReflectionComponents;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class ReflectionComponentStringBinding : Binding
    {
        [SerializeField]
        public ReflectionComponentString FallbackValue
            = new ReflectionComponentString();

        private ReflectionComponentString bindedValue = new ReflectionComponentString();

        public override Type BindingType => typeof(ReflectionComponentString);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentString GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
