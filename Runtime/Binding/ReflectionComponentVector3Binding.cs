using Juce.TweenPlayer.ReflectionComponents;
using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class ReflectionComponentVector3Binding : Binding
    {
        [SerializeField]
        public ReflectionComponentVector3 FallbackValue
            = new ReflectionComponentVector3();

        private ReflectionComponentVector3 bindedValue;

        public override Type BindingType => typeof(ReflectionComponentVector3);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentVector3 GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
