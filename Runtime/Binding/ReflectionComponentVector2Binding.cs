using Juce.TweenPlayer.ReflectionComponents;
using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class ReflectionComponentVector2Binding : Binding
    {
        [SerializeField]
        public ReflectionComponentVector2 FallbackValue
            = new ReflectionComponentVector2();

        private ReflectionComponentVector2 bindedValue = new ReflectionComponentVector2();

        public override Type BindingType => typeof(ReflectionComponentVector2);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentVector2 GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
