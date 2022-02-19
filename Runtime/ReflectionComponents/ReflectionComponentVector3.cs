using System;
using UnityEngine;

namespace Juce.TweenComponent.ReflectionComponents
{
    [Serializable]
    public class ReflectionComponentVector3 
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
