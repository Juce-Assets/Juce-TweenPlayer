using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class Vector2Binding : Binding
    {
        [SerializeField] public Vector2 FallbackValue;

        private Vector2 bindedValue;

        public Vector2 Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(Vector2);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Vector2 GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
