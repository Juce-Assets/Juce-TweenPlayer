using System;
using UnityEngine;
using Juce.TweenComponent.Utils;

namespace Juce.TweenComponent.Bindings
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
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
