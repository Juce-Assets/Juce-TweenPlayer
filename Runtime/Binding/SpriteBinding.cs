using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class SpriteBinding : Binding
    {
        [SerializeField] public Sprite FallbackValue;

        private Sprite bindedValue;

        public override Type BindingType => typeof(Sprite);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Sprite GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
