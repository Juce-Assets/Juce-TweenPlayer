﻿using System;
using UnityEngine;

namespace Juce.TweenPlayer.ReflectionComponents
{
    [Serializable]
    public class ReflectionComponentString 
    {
        public Component Component;
        public string PropertyName;

        public override string ToString()
        {
            if (Component == null)
            {
                return string.Empty;
            }

            return $"{Component.name} {PropertyName}";
        }
    }
}
