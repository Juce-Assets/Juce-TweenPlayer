using System;
using UnityEngine;
using Juce.TweenComponent.Utils;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class MonoBehaviourBinding : Binding
    {
        [SerializeField] public MonoBehaviour FallbackValue = default;

        private MonoBehaviour bindedValue;

        public override Type BindingType => typeof(MonoBehaviour);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public MonoBehaviour GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
