using Juce.TweenComponent.ReflectionComponents;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class ReflectionComponentVector3Binding : Binding
    {
        [SerializeField]
        public ReflectionComponentVector3 FallbackValue
            = new ReflectionComponentVector3();

        private ReflectionComponentVector3 bindedValue = new ReflectionComponentVector3();

        public override Type BindingType => typeof(ReflectionComponentVector3);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentVector3 GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
