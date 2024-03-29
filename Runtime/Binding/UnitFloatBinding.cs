﻿using System;
using UnityEngine;
using Juce.TweenComponent.Utils;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class UnitFloatBinding : Binding
    {
        [SerializeField] [Range(0, 1)] public float FallbackValue;

        private float bindedValue;

        public override Type BindingType => typeof(float);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);

            bindedValue = Math.Max(bindedValue, 0);
        }

        public float GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
