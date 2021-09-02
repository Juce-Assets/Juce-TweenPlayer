using Juce.Tweening;
using Juce.TweenPlayer.Utils;
using System;
using UnityEngine;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class EaseBinding : Binding
    {
        [SerializeField] public Ease FallbackValue;

        private Ease bindedValue;

        public override Type BindingType => typeof(Ease);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Ease GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
