using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class TextureBinding : Binding
    {
        [SerializeField] public Texture FallbackValue;

        private Texture bindedValue;

        public Texture Value => Binded ? bindedValue : FallbackValue;

        public override Type BindingType => typeof(Texture);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public Texture GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
