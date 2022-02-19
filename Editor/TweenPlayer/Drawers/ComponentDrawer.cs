using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Components;
using Juce.TweenComponent.Logic;
using Juce.TweenComponent.Style;
using Juce.TweenComponent.Utils;
using Juce.TweenComponent.Validation;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenComponent.Drawers
{
    public static class ComponentDrawer
    {
        public static void Draw(
           TweenPlayerEditor editor,
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

            bool found = EditorComponentUtils.TryGetCachedEditorPlayerComponent(
                editor,
                component.GetType(),
                out EditorTweenPlayerComponent editorPlayerComponent
                );

            if (!found)
            {
                return;
            }

            bool hasColor = editorPlayerComponent.Color.a > 0.0f;

            Color lastColor = GUI.backgroundColor;

            if (hasColor && editorPlayerComponent.UseColorAsBackground)
            {
                GUI.backgroundColor = editorPlayerComponent.Color;
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                bool enabled = component.Enabled;
                bool folded = component.Folded;

                string extraTitle = component.GenerateTitle();

                if (!string.IsNullOrEmpty(extraTitle))
                {
                    extraTitle = $"| {extraTitle}";
                }

                ComponentHeaderDrawer.Draw(
                    editorPlayerComponent.Name,
                    extraTitle,
                    ref enabled,
                    ref folded,
                    () => ComponentContextMenuDrawer.Draw(editor, component),
                    out Rect reorderInteractionRect,
                    out Rect reorderColorRect
                    );

                if (hasColor)
                {
                    Rect colorRect = new Rect(reorderColorRect.x + 1, reorderColorRect.y + 1, 3, 19);
                    EditorGUI.DrawRect(colorRect, editorPlayerComponent.Color);
                }

                ComponentProgressBarDrawer.Draw(editor, component);

                editor.ComponentsDragHelper.CheckDraggingItem(
                    Event.current,
                    reorderInteractionRect,
                    reorderColorRect,
                    TweenPlayerEditorStyles.ReorderRectColor,
                    componentIndex
                    );

                if(editor.ToolData.DocumentationEnabled)
                {
                    bool hasDocumentation = !string.IsNullOrEmpty(editorPlayerComponent.Documentation);

                    if (hasDocumentation)
                    {
                        EditorGUILayout.Space();

                        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                        {
                            GUILayout.Label("Documentation:", EditorStyles.boldLabel);
                            GUILayout.Label(editorPlayerComponent.Documentation, EditorStyles.wordWrappedLabel);
                        }
                    }
                }

                ValidationBuilder validationBuilder = new ValidationBuilder();

                ValidateComponentEditorBindings(
                    editor.SerializedPropertiesData.BindingEnabledProperty.boolValue,
                    componentEditorBindings,
                    validationBuilder
                    );

                component.Validate(validationBuilder);

                ValidationDrawer.Draw(validationBuilder.Build());

                component.Enabled = enabled;
                component.Folded = folded;

                if (!folded)
                {
                    EditorGUILayout.Space();
                }

                foreach (EditorBinding editorBinding in componentEditorBindings)
                {
                    UpdateBindingDataLogic.Execute(
                        editor,
                        editorBinding
                        );

                    if (folded)
                    {
                        continue;
                    }

                    BindingDrawer.Draw(editor, componentSerializedProperty, editorBinding);
                }
            }

            GUI.backgroundColor = lastColor;
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
