using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Juce.TweenPlayer.Data
{
    public class ToolData
    {
        public List<EditorTweenPlayerComponent> EditorPlayerComponents { get; } = new List<EditorTweenPlayerComponent>();
        public Dictionary<Type, EditorTweenPlayerComponent> CachedEditorPlayerComponents { get; } = new Dictionary<Type, EditorTweenPlayerComponent>();

        public List<EditorBindableData> EditorBindableDatas { get; } = new List<EditorBindableData>();

        public List<TweenPlayerComponent> ComponentsToRemove { get; } = new List<TweenPlayerComponent>();
        public List<int> ComponentsIndexToRemove { get; } = new List<int>();

        public EditorBindableData SelectedEditorBindableData { get; set; } = null;
        public bool ShowBindedDataProperties { get; set; } = false;
        public Vector2 ShowBindedDataPropertiesScrollViewPosition { get; set; } = Vector2.zero;
        public bool DocumentationEnabled { get; set; } = false;
    }
}
