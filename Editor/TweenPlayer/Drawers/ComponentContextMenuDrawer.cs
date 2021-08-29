using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Helpers;
using Juce.TweenPlayer.Utils;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer
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
                    editor.RemoveComponent(component);

                    EditorUtility.SetDirty(editor.target);

                    Event.current?.Use();
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Duplicate"), false,
                () =>
                {
                    CopyPasteComponentHelper.Copy(component);
                    CopyPasteComponentHelper.PasteAsNew(editor.ActualTarget, component);

                    EditorUtility.SetDirty(editor.target);

                    Event.current?.Use();
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Copy"), false, () => CopyPasteComponentHelper.Copy(component));

            if (CopyPasteComponentHelper.HasCopiedComponents)
            {
                menu.AddItem(new GUIContent("Paste as new"), false, () =>
                {
                    CopyPasteComponentHelper.PasteAsNew(editor.ActualTarget, component);

                    EditorUtility.SetDirty(editor.target);

                    Event.current?.Use();
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste as new"), false);
            }

            if (CopyPasteComponentHelper.HasCopiedComponents && CopyPasteComponentHelper.CanPasteValues(component))
            {
                menu.AddItem(new GUIContent("Paste values"), false, () =>
                {
                    CopyPasteComponentHelper.PasteValues(component);

                    EditorUtility.SetDirty(editor.target);

                    Event.current?.Use();
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste values"), false);
            }

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Collapse All"), false, () =>
            {
                ComponentUtils.CollapseAll(editor);

                EditorUtility.SetDirty(editor.target);

                Event.current?.Use();
            });

            menu.AddItem(new GUIContent("Expand All"), false, () =>
            {
                ComponentUtils.ExpandAll(editor);

                EditorUtility.SetDirty(editor.target);

                Event.current?.Use();
            });

            menu.ShowAsContext();
        }

    }
}
