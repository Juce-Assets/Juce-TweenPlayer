using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Reflection;
using Juce.TweenPlayer.Validation;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer
{
    public static class EditorComponentsDrawUtils
    {
        public static void DrawComponents(TweenPlayerEditor bindingPlayerEditor)
        {
            if (bindingPlayerEditor.BindingPlayerComponentsProperty.arraySize == 0)
            {
                EditorGUILayout.LabelField("No components added. Press Add Component to add a new " +
                    "twening component", EditorStyles.wordWrappedLabel);
            }
            else
            {
                EditorGUILayout.LabelField("Components:");
            }

            for (int i = 0; i < bindingPlayerEditor.BindingPlayerComponentsProperty.arraySize; ++i)
            {
                TweenPlayerComponent component = bindingPlayerEditor.ActualTarget.BindingPlayerComponents[i];

                SerializedProperty componentSerializedProperty = bindingPlayerEditor.BindingPlayerComponentsProperty.GetArrayElementAtIndex(i);

                if(component == null)
                {
                    DrawNullComponent(bindingPlayerEditor, i);
                    continue;
                }

                DrawComponent(
                    bindingPlayerEditor,
                    component,
                    componentSerializedProperty,
                    i
                    );
            }

            Event e = Event.current;

            // Finish dragging
            int startIndex;
            int endIndex;
            bool dragged = bindingPlayerEditor.ComponentsDraggable.ResolveDragging(e, out startIndex, out endIndex);

            if (dragged)
            {
                bindingPlayerEditor.ReorderComponent(startIndex, endIndex);

                EditorUtility.SetDirty(bindingPlayerEditor.target);
            }
        }

        private static void DrawNullComponent(TweenPlayerEditor bindingPlayerEditor, int componentIndex)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Null component");

                if(GUILayout.Button("Remove"))
                {
                    bindingPlayerEditor.RemoveComponent(componentIndex);
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawComponent(
            TweenPlayerEditor bindingPlayerEditor,
            TweenPlayerComponent component,
            SerializedProperty componentSerializedProperty,
            int componentIndex
            )
        {
            List<FieldInfo> fields = ReflectionUtils.GetFields(component.GetType(), typeof(Binding));

            List<EditorBinding> componentEditorBindings = new List<EditorBinding>();

            foreach (FieldInfo field in fields)
            {
                Binding bindingInstance = (Binding)field.GetValue(component);

                componentEditorBindings.Add(new EditorBinding(
                    bindingInstance.BindingType,
                    field.Name,
                    bindingInstance
                    ));
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                bool enabled = component.Enabled;
                bool folded = component.Folded;

                bool found = bindingPlayerEditor.TryGetCachedEditorPlayerComponent(
                    component.GetType(),
                    out EditorTweenPlayerComponent editorPlayerComponent
                    );

                if (!found)
                {
                    return;
                }

                string extraTitle = component.GenerateTitle();

                if (!string.IsNullOrEmpty(extraTitle))
                {
                    extraTitle = $"[{extraTitle}]";
                }

                EditorComponentDrawUtils.DrawComponentHeader(
                    editorPlayerComponent.Name, 
                    extraTitle, 
                    ref enabled, 
                    ref folded, 
                    () => EditorComponentContextMenuDrawUtils.ShowComponentContextMenu(bindingPlayerEditor, component),
                    out Rect reorderInteractionRect,
                    out Rect reorderColorRect
                    );

                EditorComponentDrawUtils.DrawComponentProgressBar(component);

                bindingPlayerEditor.ComponentsDraggable.CheckDraggingItem(
                    Event.current,
                    reorderInteractionRect,
                    reorderColorRect,
                    TweenPlayerEditorStyles.ReorderRectColor,
                    componentIndex
                    );

                ValidationBuilder validationBuilder = new ValidationBuilder();

                ValidateComponentEditorBindings(
                    bindingPlayerEditor.BindingEnabledProperty.boolValue,
                    componentEditorBindings,
                    validationBuilder
                    );

                component.Validate(validationBuilder);

                EditorComponentDrawUtils.DrawValidation(validationBuilder.Build());

                component.Enabled = enabled;
                component.Folded = folded;

                if(!folded)
                {
                    EditorGUILayout.Space();
                }

                foreach (EditorBinding editorBinding in componentEditorBindings)
                {
                    EditorBindingDrawUtils.UpdateBindingData(
                        bindingPlayerEditor,
                        editorBinding
                        );

                    if (folded)
                    {
                        continue;
                    }

                    EditorBindingDrawUtils.DrawEditorBinding(bindingPlayerEditor, componentSerializedProperty, editorBinding);
                }
            }
        }

        private static void ValidateComponentEditorBindings(
            bool bindingEnabled,
            List<EditorBinding> editorBindings,
            ValidationBuilder validationBuilder
            )
        {
            foreach (EditorBinding editorBinding in editorBindings)
            {
                if (bindingEnabled && editorBinding.Binding.WantsToBeBinded && !editorBinding.Binding.Binded)
                {
                    validationBuilder.LogError($"Invalid binding for {editorBinding.Name}");
                    validationBuilder.SetError();
                }
            }
        }
    }
}
