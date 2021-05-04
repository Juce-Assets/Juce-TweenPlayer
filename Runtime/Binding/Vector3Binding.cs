using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class Vector3Binding : Binding
    {
        [SerializeField] public Vector3 FallbackValue;

        private Vector3 bindedValue;

        public Vector3 Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(Vector3);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Vector3 GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
