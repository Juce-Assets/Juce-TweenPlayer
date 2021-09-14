using System;

namespace Juce.TweenPlayer.BindableData
{
    public class BindableDataAttribute : Attribute
    {
        public string Name { get; }
        public string MenuPath { get; }
        public string Uid { get; }

        public BindableDataAttribute(string name, string menuPath, string uid)
        {
            Name = name;
            MenuPath = menuPath;
            Uid = uid;
        }
    }
}
