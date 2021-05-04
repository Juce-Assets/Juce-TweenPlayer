using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class TextMeshProBinding : Binding
    {
        [SerializeField] public TMPro.TextMeshProUGUI FallbackValue;

        private TMPro.TextMeshProUGUI bindedValue;

        public override Type BindingType => typeof(TMPro.TextMeshProUGUI);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public TMPro.TextMeshProUGUI GetValue()
        {
            return BindingUtils.TrGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
