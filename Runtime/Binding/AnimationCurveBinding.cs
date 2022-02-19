using Juce.Tweening;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
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
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
