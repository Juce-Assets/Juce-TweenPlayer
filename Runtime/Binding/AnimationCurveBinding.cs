using Juce.Tween;
using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class AnimationCurveBinding : Binding
    {
        [SerializeField] public AnimationCurve FallbackValue = AnimationCurve.Linear(0, 0, 1, 1);

        private AnimationCurve bindedValue;

        public override Type BindingType => typeof(AnimationCurve);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public AnimationCurve GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
