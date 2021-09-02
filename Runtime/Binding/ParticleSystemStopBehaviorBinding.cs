using System;
using UnityEngine;
using Juce.TweenPlayer.Utils;

namespace Juce.TweenPlayer.Bindings
{
    [System.Serializable]
    public class ParticleSystemStopBehaviorBinding : Binding
    {
        [SerializeField] public ParticleSystemStopBehavior FallbackValue;

        private ParticleSystemStopBehavior bindedValue;

        public override Type BindingType => typeof(ParticleSystemStopBehavior);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ParticleSystemStopBehavior GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
