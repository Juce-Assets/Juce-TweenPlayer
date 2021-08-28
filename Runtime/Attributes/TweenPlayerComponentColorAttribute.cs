using System;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    public class TweenPlayerComponentColorAttribute : Attribute
    {
        public Color Color { get; }

        public TweenPlayerComponentColorAttribute(float r, float g, float b)
        {
            Color = new Color(r, g, b, 1f);
        }
    }
}
