using System;
using UnityEngine;
using Juce.TweenComponent.Utils;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class Vector4Binding : Binding
    {
        [SerializeField] public Vector4 FallbackValue;

        private Vector4 bindedValue;

        public Vector4 Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(Vector4);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Vector4 GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
