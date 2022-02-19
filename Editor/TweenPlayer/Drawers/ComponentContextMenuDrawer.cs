using Juce.TweenComponent.Components;
using Juce.TweenComponent.Helpers;
using Juce.TweenComponent.Logic;
using Juce.TweenComponent.Utils;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenComponent
{
    public static class ComponentContextMenuDrawer
    {
        public static void Draw(
            TweenPlayerEditor editor, 
            TweenPlayerComponent component
            )
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Remove"), false,
                () =>
                {
                    RemoveComponentLogic.Execute(editor, component);

                    EditorUtility.SetDirty(editor.target);
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Duplicate"), false,
                () =>
                {
                    CopyPasteComponentHelper.Copy(component);
                    CopyPasteComponentHelper.PasteAsNew(editor, component);

                    EditorUtility.SetDirty(editor.target);
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Copy"), false, () => CopyPasteComponentHelper.Copy(component));

            if (CopyPasteComponentHelper.HasCopiedComponents)
            {
                menu.AddItem(new GUIContent("Paste As New"), false, () =>
                {
                    CopyPasteComponentHelper.PasteAsNew(editor, component);

                    EditorUtility.SetDirty(editor.target);
                });

                menu.AddItem(new GUIContent("Paste As New (on top)"), false, () =>
                {
                    CopyPasteComponentHelper.PasteAsNew(editor, component, destinationOffset: -1);

                    EditorUtility.SetDirty(editor.target);
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste As New"), false);
                menu.AddDisabledItem(new GUIContent("Paste As New (on top)"), false);
            }

            if (CopyPasteComponentHelper.HasCopiedComponents && CopyPasteComponentHelper.CanPasteValues(component))
            {
                menu.AddItem(new GUIContent("Paste Values"), false, () =>
                {
                    CopyPasteComponentHelper.PasteValues(editor, component);

                    EditorUtility.SetDirty(editor.target);
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste Values"), false);
            }

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Collapse All"), false, () =>
            {
                ComponentUtils.CollapseAll(editor);

                EditorUtility.SetDirty(editor.target);
            });

            menu.AddItem(new GUIContent("Expand All"), false, () =>
            {
                ComponentUtils.ExpandAll(editor);

                EditorUtility.SetDirty(editor.target);
            });

            menu.AddSeparator("");

            if (!editor.ToolData.DocumentationEnabled)
            {
                menu.AddItem(new GUIContent("Show Documentation"), false, () =>
                {
                    editor.ToolData.DocumentationEnabled = true;
                });
            }
            else
            {
                menu.AddItem(new GUIContent("Hide Documentation"), false, () =>
                {
                    editor.ToolData.DocumentationEnabled = false;
                });
            }

            menu.ShowAsContext();
        }

    }
}
