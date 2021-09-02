using Juce.TweenPlayer.ReflectionComponents;
using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class ReflectionComponentColorBinding : Binding
    {
        [SerializeField]
        public ReflectionComponentColor FallbackValue
            = new ReflectionComponentColor();

        private ReflectionComponentColor bindedValue = new ReflectionComponentColor();

        public override Type BindingType => typeof(ReflectionComponentColor);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentColor GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
