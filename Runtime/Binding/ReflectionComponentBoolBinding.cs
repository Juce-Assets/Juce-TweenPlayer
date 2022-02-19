using Juce.TweenComponent.ReflectionComponents;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class ReflectionComponentBoolBinding : Binding
    {
        [SerializeField]
        public ReflectionComponentBool FallbackValue
            = new ReflectionComponentBool();

        private ReflectionComponentBool bindedValue = new ReflectionComponentBool();

        public override Type BindingType => typeof(ReflectionComponentBool);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentBool GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
