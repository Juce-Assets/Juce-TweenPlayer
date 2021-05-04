using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Helpers;
using Juce.TweenPlayer.Reflection;
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
        private readonly List<EditorTweenPlayerComponent> editorPlayerComponents = new List<EditorTweenPlayerComponent>();
        private readonly Dictionary<Type, EditorTweenPlayerComponent> cachedEditorPlayerComponents = new Dictionary<Type, EditorTweenPlayerComponent>();

        private List<EditorBindableData> editorBindableDatas = new List<EditorBindableData>();

        private readonly List<TweenPlayerComponent> componentsToRemove = new List<TweenPlayerComponent>();
        private readonly List<int> componentsIndexToRemove = new List<int>();

        private SerializedProperty executionModeProperty;
        private SerializedProperty loopModeProperty;
        private SerializedProperty loopResetModeProperty;
        private SerializedProperty loopsProperty;
        private SerializedProperty bindingPlayerComponentsProperty;
        private SerializedProperty bindingEnabledProperty;
        private SerializedProperty bindableDataUidProperty;

        public IReadOnlyList<EditorBindableData> EditorBindableDatas => editorBindableDatas;

        public TweenPlayer ActualTarget { get; private set; }

        public DragHelper ComponentsDraggable { get; } = new DragHelper();

        public SerializedProperty ExecutionModeProperty => executionModeProperty;
        public SerializedProperty LoopModeProperty => loopModeProperty;
        public SerializedProperty LoopResetModeProperty => loopResetModeProperty;
        public SerializedProperty LoopsProperty => loopsProperty;
        public SerializedProperty BindingPlayerComponentsProperty => bindingPlayerComponentsProperty;
        public SerializedProperty BindingEnabledProperty => bindingEnabledProperty;
        public SerializedProperty BindableDataUidProperty => bindableDataUidProperty;

        public EditorBindableData SelectedEditorBindableData { get; set; }
        public bool ShowBindedDataProperties { get; set; }
        public Vector2 ShowBindedDataPropertiesScrollViewPosition { get; set; }

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

            TweenPlayerEditorDrawUtils.DrawGeneralProgressBar(this);

            EditorGUILayout.Space(1);

            TweenPlayerEditorDrawUtils.DrawExecutionMode(this);

            EditorGUILayout.Space(1);

            TweenPlayerEditorDrawUtils.DrawLoopMode(this);

            EditorGUILayout.Space(1);

            TweenPlayerEditorDrawUtils.DrawSelectedBindableData(this);

            EditorGUILayout.Space(1);

            EditorComponentsDrawUtils.DrawComponents(this);

            EditorGUILayout.Space(1);

            DrawAddComponent();

            EditorGUILayout.Space(1);

            TweenPlayerEditorDrawUtils.DrawPlaybackControls(this);

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
            bindingPlayerComponentsProperty = serializedObject.FindProperty("BindingPlayerComponents");
            bindingEnabledProperty = serializedObject.FindProperty("bindingEnabled");
            bindableDataUidProperty = serializedObject.FindProperty("bindableDataUid");
        }

        public void SetBindableDataUid(string uid)
        {
            bindableDataUidProperty.stringValue = uid;
            serializedObject.ApplyModifiedProperties();
        }

        private void GatherEditorBindingPlayerComponents()
        {
            List<Type> types = ReflectionUtils.GetInheritedTypes(typeof(TweenPlayerComponent));

            foreach (Type type in types)
            {
                bool found = ReflectionUtils.TryGetAttribute(type, out TweenPlayerComponentAttribute attribute);

                if(!found)
                {
                    continue; ;
                }

                editorPlayerComponents.Add(new EditorTweenPlayerComponent(type, attribute.Name, attribute.MenuPath));
            }
        }

        public bool TryGetCachedEditorPlayerComponent(Type componentType, out EditorTweenPlayerComponent editorPlayerComponent)
        {
            bool found = cachedEditorPlayerComponents.TryGetValue(componentType, out editorPlayerComponent);

            if (found)
            {
                return true;
            }

            foreach (EditorTweenPlayerComponent component in editorPlayerComponents)
            {
                if (componentType == component.Type)
                {
                    cachedEditorPlayerComponents.Add(componentType, component);

                    editorPlayerComponent = component;
                    return true;
                }
            }

            editorPlayerComponent = null;
            return false;
        }

        private void GatherEditorBindableData()
        {
            editorBindableDatas = EditorBindableDataUtils.GatherEditorBindableData();
        }

        private void DrawAddComponent()
        {
            if (GUILayout.Button("Add component"))
            {
                ShowComponentsMenu();
            }
        }

        public void ShowBindableDatasMenu()
        {
            GenericMenu menu = new GenericMenu();

            foreach (EditorBindableData bindableDatas in editorBindableDatas)
            {
                menu.AddItem(new GUIContent($"{bindableDatas.MenuPath}"),
                false, () => SetBindableDataUid(bindableDatas.Uid));
            }

            menu.ShowAsContext();
        }

        private void ShowComponentsMenu()
        {
            GenericMenu menu = new GenericMenu();

            foreach(EditorTweenPlayerComponent component in editorPlayerComponents)
            {
                menu.AddItem(new GUIContent($"{component.MenuPath}"),
                false, () => OnAddComponentSelected(component.Type));
            }

            menu.ShowAsContext();
        }

        private void OnAddComponentSelected(Type componentType)
        {
            ActualTarget.AddComponent(componentType);
        }

        public void RemoveComponent(TweenPlayerComponent component) 
        {
            componentsToRemove.Add(component);
        }

        public void RemoveComponent(int index)
        {
            componentsIndexToRemove.Add(index);
        }

        public void ReorderComponent(int componentIndex, int newComponentIndex)
        {
            if (componentIndex == newComponentIndex)
            {
                return;
            }

            if (componentIndex < 0 || componentIndex >= ActualTarget.BindingPlayerComponents.Count)
            {
                return;
            }

            newComponentIndex = Math.Min(newComponentIndex, ActualTarget.BindingPlayerComponents.Count - 1);
            newComponentIndex = Math.Max(newComponentIndex, 0);

            TweenPlayerComponent component = ActualTarget.BindingPlayerComponents[componentIndex];

            ActualTarget.BindingPlayerComponents.RemoveAt(componentIndex);
            ActualTarget.BindingPlayerComponents.Insert(newComponentIndex, component);
        }

        private void ActuallyRemoveComponents()
        {
            foreach (int componentIndex in componentsIndexToRemove)
            {
                ActualTarget.BindingPlayerComponents.RemoveAt(componentIndex);
            }

            foreach (TweenPlayerComponent component in componentsToRemove)
            {
                ActualTarget.BindingPlayerComponents.Remove(component);
            }

            componentsIndexToRemove.Clear();
            componentsToRemove.Clear();
        }
    }
}
