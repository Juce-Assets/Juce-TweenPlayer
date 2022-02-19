using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class MaterialBinding : Binding
    {
        [SerializeField] public Material FallbackValue;

        private Material bindedValue;

        public Material Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(Material);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Material GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
