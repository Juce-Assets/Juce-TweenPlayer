using System;
using UnityEngine;

namespace Juce.TweenPlayer.Attributes
{
    public class TweenPlayerComponentColorAttribute : Attribute
    {
        public Color Color { get; }
        public bool UseAsBackground { get; }

        public TweenPlayerComponentColorAttribute(float r, float g, float b, bool useAsBackground = false)
        {
            Color = new Color(r, g, b, 1f);
            UseAsBackground = useAsBackground;
        }
    }
}
