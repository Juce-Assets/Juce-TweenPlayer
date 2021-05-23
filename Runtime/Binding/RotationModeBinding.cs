using Juce.Tween;
using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class RotationModeBinding : Binding
    {
        [SerializeField] public RotationMode FallbackValue;

        private RotationMode bindedValue;

        public RotationMode Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(RotationMode);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public RotationMode GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
