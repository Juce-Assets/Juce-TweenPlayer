using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class TransformBinding : Binding
    {
        [SerializeField] public Transform FallbackValue;

        private Transform bindedValue;

        public Transform Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(Transform);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Transform GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
