using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Components;
using Juce.TweenComponent.Data;
using Juce.TweenComponent.Drawers;
using Juce.TweenComponent.Helpers;
using Juce.TweenComponent.Logic;

using UnityEditor;
using UnityEngine;

namespace Juce.TweenComponent
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

            if (Event.current.type != EventType.Layout)
            {
                ActuallyRemoveComponentsLogic.Execute(this);
            }

            if (SerializedPropertiesData.ShowProgressBarsProperty.boolValue && Application.isPlaying)
            {
                Repaint();
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(ActualTarget.gameObject);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
