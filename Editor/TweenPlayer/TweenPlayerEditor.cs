using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Data;
using Juce.TweenPlayer.Drawers;
using Juce.TweenPlayer.Helpers;
using Juce.TweenPlayer.Logic;

using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer
{
    [CustomEditor(typeof(TweenPlayer))]
    public class TweenPlayerEditor : Editor
    {
        public TweenPlayer ActualTarget { get; private set; }

        public ToolData ToolData { get; } = new ToolData();
        public SerializedPropertiesData SerializedPropertiesData { get; } = new SerializedPropertiesData();
        public DragHelper ComponentsDragHelper { get; } = new DragHelper();

        private void OnEnable()
        {
            ActualTarget = (TweenPlayer)target;

            GatherSerializedPropertiesLogic.Execute(this);
            GatherEditorBindingPlayerComponentsLogic.Execute(this);
            GatherEditorBindableDataLogic.Execute(this);
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

            if (ToolData.ProgressBarsEnabled && Application.isPlaying)
            {
                Repaint();
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(ActualTarget.gameObject);
            }

            serializedObject.ApplyModifiedProperties();
        }

        public void RemoveComponent(TweenPlayerComponent component) 
        {
            ToolData.ComponentsToRemove.Add(component);
        }

        public void RemoveComponent(int index)
        {
            ToolData.ComponentsIndexToRemove.Add(index);
        }

        private void ActuallyRemoveComponents()
        {
            foreach (int componentIndex in ToolData.ComponentsIndexToRemove)
            {
                ActualTarget.Components.RemoveAt(componentIndex);
            }

            foreach (TweenPlayerComponent component in ToolData.ComponentsToRemove)
            {
                ActualTarget.Components.Remove(component);
            }

            ToolData.ComponentsIndexToRemove.Clear();
            ToolData.ComponentsToRemove.Clear();
        }
    }
}
