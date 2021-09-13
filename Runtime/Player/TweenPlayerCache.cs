using Juce.TweenPlayer.Components;
using System.Collections.Generic;
using System.Reflection;

namespace Juce.TweenPlayer
{
    public class TweenPlayerCache
    {
        public bool HasBindableData { get; set; } = false;
        public List<FieldInfo> BindableDataFields { get; } = new List<FieldInfo>();
        public List<PropertyInfo> BindableDataProperties { get; } = new List<PropertyInfo>();

        public bool HasComponentFields { get; set; } = false;
        public Dictionary<TweenPlayerComponent, List<FieldInfo>> ComponentFields { get; } = new Dictionary<TweenPlayerComponent, List<FieldInfo>>(); 

        public void Clear()
        {
            HasBindableData = false;
            BindableDataFields.Clear();
            BindableDataProperties.Clear();

            HasComponentFields = false;
            ComponentFields.Clear();
        }
    }
}
