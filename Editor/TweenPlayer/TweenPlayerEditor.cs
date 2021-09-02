using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Drawers;
using Juce.TweenPlayer.Helpers;
using Juce.TweenPlayer.Utils;
using System;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer
{
    [CustomEditor(typeof(TweenPlayer))]
    public class TweenPlayerEditor : Editor
    {
        private List<EditorTweenPlayerComponent> editorPlayerComponents = new List<EditorTweenPlayerComponent>();
        private readonly Dictionary<Type, EditorTweenPlayerComponent> cachedEditorPlayerComponents = new Dictionary<Type, EditorTweenPlayerComponent>();

        private List<EditorBindableData> editorBindableDatas = new List<EditorBindableData>();

        private readonly List<TweenPlayerComponent> componentsToRemove = new List<TweenPlayerComponent>();
        private readonly List<int> componentsIndexToRemove = new List<int>();

        private SerializedProperty executionModeProperty;
        private SerializedProperty loopModeProperty;
        private SerializedProperty loopResetModeProperty;
        private SerializedProperty loopsProperty;
        private SerializedProperty componentsProperty;
        private SerializedProperty bindingEnabledProperty;
        private SerializedProperty bindableDataUidProperty;

        public IReadOnlyList<EditorTweenPlayerComponent> EditorPlayerComponents => editorPlayerComponents;
        public Dictionary<Type, EditorTweenPlayerComponent> CachedEditorPlayerComponents => cachedEditorPlayerComponents;
        public IReadOnlyList<EditorBindableData> EditorBindableDatas => editorBindableDatas;

        public TweenPlayer ActualTarget { get; private set; }

        public DragHelper ComponentsDraggable { get; } = new DragHelper();

        public SerializedProperty ExecutionModeProperty => executionModeProperty;
        public SerializedProperty LoopModeProperty => loopModeProperty;
        public SerializedProperty LoopResetModeProperty => loopResetModeProperty;
        public SerializedProperty LoopsProperty => loopsProperty;
        public SerializedProperty ComponentsProperty => componentsProperty;
        public SerializedProperty BindingEnabledProperty => bindingEnabledProperty;
        public SerializedProperty BindableDataUidProperty => bindableDataUidProperty;

        public EditorBindableData SelectedEditorBindableData { get; set; }
        public bool ShowBindedDataProperties { get; set; }
        public Vector2 ShowBindedDataPropertiesScrollViewPosition { get; set; }
        public bool DocumentationEnabled { get; set; } 

        private void OnEnable()
        {
            ActualTarget = (TweenPlayer)target;

            GatherProperties();

            GatherEditorBindingPlayerComponents();
            GatherEditorBindableData();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            serializedObject.Update();

            GeneralProgressBarDrawer.Draw(this);

            EditorGUILayout.Space(1);

            ExecutionModeDrawer.Draw(this);

            EditorGUILayout.Space(1);

            LoopModeDrawer.Draw(this);

            EditorGUILayout.Space(1);

            BindableDataDrawer.Draw(this);

            EditorGUILayout.Space(1);

            ComponentsDrawer.Draw(this);

            EditorGUILayout.Space(1);

            BottomComponentControlsDrawer.Draw(this);

            EditorGUILayout.Space(1);

            PlaybackControlsDrawer.Draw(this);

            ActuallyRemoveComponents();

            if (Application.isPlaying)
            {
                Repaint();
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(ActualTarget.gameObject);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void GatherProperties()
        {
            executionModeProperty = serializedObject.FindProperty("executionMode");
            loopModeProperty = serializedObject.FindProperty("loopMode");
            loopResetModeProperty = serializedObject.FindProperty("loopResetMode");
            loopsProperty = serializedObject.FindProperty("loops");
            componentsProperty = serializedObject.FindProperty("Components");
            bindingEnabledProperty = serializedObject.FindProperty("bindingEnabled");
            bindableDataUidProperty = serializedObject.FindProperty("bindableDataUid");
        }

        private void GatherEditorBindingPlayerComponents()
        {
            editorPlayerComponents = EditorComponentUtils.GatherEditorComponents();
        }

        private void GatherEditorBindableData()
        {
            editorBindableDatas = EditorBindableDataUtils.GatherEditorBindableData();
        }

        public void RemoveComponent(TweenPlayerComponent component) 
        {
            componentsToRemove.Add(component);
        }

        public void RemoveComponent(int index)
        {
            componentsIndexToRemove.Add(index);
        }

        private void ActuallyRemoveComponents()
        {
            foreach (int componentIndex in componentsIndexToRemove)
            {
                ActualTarget.Components.RemoveAt(componentIndex);
            }

            foreach (TweenPlayerComponent component in componentsToRemove)
            {
                ActualTarget.Components.Remove(component);
            }

            componentsIndexToRemove.Clear();
            componentsToRemove.Clear();
        }
    }
}
