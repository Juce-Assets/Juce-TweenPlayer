﻿using Juce.TweenComponent.ReflectionComponents;
using Juce.TweenComponent.Utils;
using System;
using UnityEngine;

namespace Juce.TweenComponent.Bindings
{
    [System.Serializable]
    public class ReflectionComponentIntBinding : Binding
    {
        [SerializeField]
        public ReflectionComponentInt FallbackValue
            = new ReflectionComponentInt();

        private ReflectionComponentInt bindedValue = new ReflectionComponentInt();

        public override Type BindingType => typeof(ReflectionComponentInt);

        public override void SetBindedValue(object objectValue)
        {
            BindingUtils.TrySetBindedValue(objectValue, ref bindedValue);
        }

        public ReflectionComponentInt GetValue()
        {
            return BindingUtils.TryGetValue(this, bindedValue, FallbackValue);
        }

        public override string ToString()
        {
            return BindingUtils.ToString(this, FallbackValue);
        }
    }
}
