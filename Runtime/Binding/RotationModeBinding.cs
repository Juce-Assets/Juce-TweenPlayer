using Juce.Tweening;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
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
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
