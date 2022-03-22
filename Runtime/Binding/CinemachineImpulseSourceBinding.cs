#if JUCE_CINEMACHINE_EXTENSIONS

using Cinemachine;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class CinemachineImpulseSourceBinding : Binding
    {
        [SerializeField] public CinemachineImpulseSource FallbackValue;

        private CinemachineImpulseSource bindedValue;

        public override Type BindingType => typeof(CinemachineImpulseSource);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public CinemachineImpulseSource GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}

#endif
