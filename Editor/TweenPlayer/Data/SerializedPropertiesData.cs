﻿using UnityEditor;

namespace Juce.TweenPlayer.Data
{
    public class SerializedPropertiesData
    {
        public SerializedProperty ExecutionModeProperty { get; set; }
        public SerializedProperty LoopModeProperty { get; set; }
        public SerializedProperty LoopResetModeProperty { get; set; }
        public SerializedProperty LoopsProperty { get; set; }
        public SerializedProperty ComponentsProperty { get; set; }
        public SerializedProperty BindingEnabledProperty { get; set; }
        public SerializedProperty BindableDataUidProperty { get; set; }
    }
}