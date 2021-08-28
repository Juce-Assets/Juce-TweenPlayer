using System;

namespace Juce.TweenPlayer.Components
{
    public class TweenPlayerComponentAttribute : Attribute
    {
        public string Name { get; }
        public string MenuPath { get; }

        public TweenPlayerComponentAttribute(string name, string menuPath)
        {
            Name = name;
            MenuPath = menuPath;
        }
    }
}
