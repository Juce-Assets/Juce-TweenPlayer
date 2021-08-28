using Juce.TweenPlayer.Components;
using Juce.TweenPlayer.Helpers;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer
{
    public static class EditorComponentContextMenuDrawUtils
    {
        public static void ShowComponentContextMenu(
            TweenPlayerEditor bindingPlayerEditor, 
            TweenPlayerComponent component
            )
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Remove"), false,
                () =>
                {
                    bindingPlayerEditor.RemoveComponent(component);

                    EditorUtility.SetDirty(bindingPlayerEditor.target);

                    Event.current?.Use();
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Duplicate"), false,
                () =>
                {
                    CopyPasteComponentHelper.Copy(component);
                    CopyPasteComponentHelper.PasteAsNew(bindingPlayerEditor.ActualTarget, component);

                    EditorUtility.SetDirty(bindingPlayerEditor.target);

                    Event.current?.Use();
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Copy"), false, () => CopyPasteComponentHelper.Copy(component));

            if (CopyPasteComponentHelper.HasCopiedComponent)
            {
                menu.AddItem(new GUIContent("Paste as new"), false, () =>
                {
                    CopyPasteComponentHelper.PasteAsNew(bindingPlayerEditor.ActualTarget, component);

                    EditorUtility.SetDirty(bindingPlayerEditor.target);

                    Event.current?.Use();
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste as new"), false);
            }

            if (CopyPasteComponentHelper.HasCopiedComponent && CopyPasteComponentHelper.CanPasteValues(component))
            {
                menu.AddItem(new GUIContent("Paste values"), false, () =>
                {
                    CopyPasteComponentHelper.PasteValues(component);

                    EditorUtility.SetDirty(bindingPlayerEditor.target);

                    Event.current?.Use();
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste values"), false);
            }

            menu.ShowAsContext();
        }

    }
}
