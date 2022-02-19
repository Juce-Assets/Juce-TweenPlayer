using System;
using UnityEngine;
using Juce.TweenComponent.Utils;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class GameObjectBinding : Binding
    {
        [SerializeField] public GameObject FallbackValue;

        private GameObject bindedValue;

        public override Type BindingType => typeof(GameObject);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public GameObject GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
