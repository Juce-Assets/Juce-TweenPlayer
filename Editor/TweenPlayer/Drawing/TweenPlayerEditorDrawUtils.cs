using Juce.Tween;
using Juce.TweenPlayer.BindableData;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer
{
    public static class TweenPlayerEditorDrawUtils 
    {
        public static GUIStyle SmallTickbox { get; } = new GUIStyle("ShurikenToggle");

        //public static void DrawSplitter(float height = 1.0f, float leftOffset = 0.0f, float rightOffset = 0.0f)
        //{
        //    Rect rect = GUILayoutUtility.GetRect(1f, height);

        //    rect.x += leftOffset;
        //    rect.width += (-leftOffset) + rightOffset;

        //    EditorGUI.DrawRect(rect, SplitterColor);
        //}

        public static void DrawExecutionMode(TweenPlayerEditor bindingPlayerEditor)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label($"Execution mode:");

                ExecutionMode executionMode = (ExecutionMode)bindingPlayerEditor.ExecutionModeProperty.enumValueIndex;
                ExecutionMode newExecutionMode = (ExecutionMode)EditorGUILayout.EnumPopup("", executionMode);

                if (executionMode != newExecutionMode)
                {
                    bindingPlayerEditor.ExecutionModeProperty.enumValueIndex = (int)newExecutionMode;

                    bindingPlayerEditor.serializedObject.ApplyModifiedProperties();
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        public static void DrawLoopMode(TweenPlayerEditor bindingPlayerEditor)
        {
            LoopMode loopMode = (LoopMode)bindingPlayerEditor.LoopModeProperty.enumValueIndex;

            bool loopingEnabled = loopMode == LoopMode.UntilManuallyStopped || loopMode == LoopMode.XTimes;
            bool needsLoopsCount = loopMode == LoopMode.XTimes;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label($"Loop mode:");

                    LoopMode newLoopMode = (LoopMode)EditorGUILayout.EnumPopup("", loopMode);
                    bindingPlayerEditor.LoopModeProperty.enumValueIndex = (int)newLoopMode;

                    loopingEnabled = newLoopMode == LoopMode.UntilManuallyStopped || newLoopMode == LoopMode.XTimes;
                    needsLoopsCount = newLoopMode == LoopMode.XTimes;
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (loopingEnabled)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Label($"Loop reset mode:");

                        LoopResetMode resetMode = (LoopResetMode)bindingPlayerEditor.LoopResetModeProperty.enumValueIndex;
                        LoopResetMode newResetMode = (LoopResetMode)EditorGUILayout.EnumPopup("", resetMode);
                        bindingPlayerEditor.LoopResetModeProperty.enumValueIndex = (int)newResetMode;
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }

                if (needsLoopsCount)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Label($"Loops:");

                        int loops = bindingPlayerEditor.LoopsProperty.intValue;
                        int newLoops = (int)EditorGUILayout.IntField("", loops);
                        bindingPlayerEditor.LoopsProperty.intValue = newLoops;
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        public static void DrawSelectedBindableData(TweenPlayerEditor bindingPlayerEditor)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label($"Bindings enabled");
                    EditorGUILayout.PropertyField(bindingPlayerEditor.BindingEnabledProperty, new GUIContent(""));
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (!bindingPlayerEditor.BindingEnabledProperty.boolValue)
                {
                    return;
                }

                if (bindingPlayerEditor.EditorBindableDatas.Count == 0)
                {
                    EditorGUILayout.HelpBox($"There is no avaliable {nameof(IBindableData)} on the project. Please," +
                        $"create a class that inherits from {nameof(IBindableData)}.", MessageType.Warning);
                    return;
                }

                EditorBindableData editorBindableData = GetEditorBindableData(bindingPlayerEditor, bindingPlayerEditor.BindableDataUidProperty.stringValue);
                bindingPlayerEditor.SelectedEditorBindableData = editorBindableData;

                EditorGUILayout.BeginHorizontal();
                {
                    if (editorBindableData != null)
                    {
                        GUILayout.Label($"Binded data:");
                        GUILayout.Label($"{editorBindableData.Name}", EditorStyles.boldLabel);
                    }

                    if (GUILayout.Button("Select"))
                    {
                        bindingPlayerEditor.ShowBindableDatasMenu();
                    }

                    if (editorBindableData != null)
                    {
                        if (GUILayout.Button("X"))
                        {
                            bindingPlayerEditor.BindableDataUidProperty.stringValue = string.Empty;
                        }
                    }
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (editorBindableData != null)
                {
                    if (!bindingPlayerEditor.ShowBindedDataProperties)
                    {
                        if (GUILayout.Button("Show properties"))
                        {
                            bindingPlayerEditor.ShowBindedDataProperties = true;
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Hide properties"))
                        {
                            bindingPlayerEditor.ShowBindedDataProperties = false;
                        }
                    }
                }

                if (bindingPlayerEditor.ShowBindedDataProperties)
                {
                    DrawBindableDataProperties(bindingPlayerEditor, editorBindableData);
                }
            }
        }

        private static void DrawBindableDataProperties(TweenPlayerEditor bindingPlayerEditor, EditorBindableData editorBindableData)
        {
            if (editorBindableData == null)
            {
                return;
            }

            EditorGUILayout.LabelField("Properties:");

            bindingPlayerEditor.ShowBindedDataPropertiesScrollViewPosition = 
                EditorGUILayout.BeginScrollView(bindingPlayerEditor.ShowBindedDataPropertiesScrollViewPosition, GUILayout.MaxHeight(150));
            {
                foreach (EditorBindableDataField field in editorBindableData.Fields)
                {
                    EditorGUILayout.LabelField($"- [{field.Type.Name}] {field.Name}");
                }

                foreach (EditorBindableDataProperty property in editorBindableData.Properties)
                {
                    EditorGUILayout.LabelField($"- [{property.Type.Name}] {property.Name}");
                }
            }
            EditorGUILayout.EndScrollView();
        }

        public static void DrawPlaybackControls(TweenPlayerEditor bindingPlayerEditor)
        {
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            {
                using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                {
                    if (GUILayout.Button("Play"))
                    {
                        bindingPlayerEditor.ActualTarget.Play();
                    }

                    if (GUILayout.Button("Complete"))
                    {
                        bindingPlayerEditor.ActualTarget.Complete();
                    }

                    if (GUILayout.Button("Kill"))
                    {
                        bindingPlayerEditor.ActualTarget.Kill();
                    }

                    if (GUILayout.Button("Replay"))
                    {
                        bindingPlayerEditor.ActualTarget.Replay();
                    }
                }
            }
            EditorGUI.EndDisabledGroup();
        }

        public static void DrawGeneralProgressBar(TweenPlayerEditor bindingPlayerEditor)
        {
            float progress = bindingPlayerEditor.ActualTarget.GetNormalizedProgress();

            if (progress > 0 && progress < 1)
            {
                DrawProgressBar(
                    bindingPlayerEditor.ActualTarget.GetNormalizedProgress(),
                    TweenPlayerEditorStyles.TaskRunningColor,
                    offsetX: -15, offsetY: -5, height: 3
                    );

                return;
            }

            if (progress >= 1)
            {
                DrawProgressBar(
                    bindingPlayerEditor.ActualTarget.GetNormalizedProgress(),
                    TweenPlayerEditorStyles.TaskFinishedColor,
                    offsetX: -15, offsetY: -5, height: 3
                    );
            }
        }

        private static EditorBindableData GetEditorBindableData(TweenPlayerEditor bindingPlayerEditor, string bindableDataUid)
        {
            for (int i = 0; i < bindingPlayerEditor.EditorBindableDatas.Count; ++i)
            {
                EditorBindableData editorBindableData = bindingPlayerEditor.EditorBindableDatas[i];

                if (string.Equals(bindableDataUid, editorBindableData.Uid))
                {
                    return editorBindableData;
                }
            }

            return null;
        }

        public static void DrawProgressBar(
            float normalizedProgress, 
            Color color, 
            float offsetX = 0, 
            float offsetY = 0,
            float height = 2
            )
        {
            Rect progressRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            progressRect.x -= 3 - offsetX;
            progressRect.y += offsetY;
            progressRect.width += 6 - (offsetX * 2);
            progressRect.height = height;

            progressRect.width *= normalizedProgress;

            EditorGUI.DrawRect(progressRect, color);
        }
    }
}
