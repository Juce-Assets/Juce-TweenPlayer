using System;
using UnityEngine;
using Juce.TweenComponent.Utils;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class LightBinding : Binding
    {
        [SerializeField] public Light FallbackValue;

        private Light bindedValue;

        public override Type BindingType => typeof(Light);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Light GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
