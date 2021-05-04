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

                    Event.current?.Use();
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Duplicate"), false,
                () =>
                {
                    CopyPasteComponentHelper.Copy(component);
                    CopyPasteComponentHelper.PasteAsNew(bindingPlayerEditor.ActualTarget, component);

                    Event.current?.Use();
                });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Copy"), false, () => CopyPasteComponentHelper.Copy(component));

            if (CopyPasteComponentHelper.HasCopiedComponent)
            {
                menu.AddItem(new GUIContent("Paste as new"), false, () =>
                {
                    CopyPasteComponentHelper.PasteAsNew(bindingPlayerEditor.ActualTarget, component);

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

                    Event.current?.Use();
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Paste values"), false);
            }

            //if (CopyPasteHelper.Instance.CanPasteAsNew())
            //{
            //    menu.AddItem(new GUIContent("Paste As New"), false, () =>
            //    {
            //        int feedbackIndex = GetFeedbackIndex(feedback);

            //        UndoHelper.Instance.BeginUndo("PasteAsNew");
            //        CopyPasteHelper.Instance.PasteFeedbackAsNew(this, feedbackIndex + 1);
            //        UndoHelper.Instance.EndUndo();
            //    });
            //}
            //else
            //{
            //    menu.AddDisabledItem(new GUIContent("Paste As New"), false);
            //}
            //menu.AddItem(new GUIContent("Duplicate As New"), false, () =>
            //{
            //    int feedbackIndex = GetFeedbackIndex(feedback);

            //    UndoHelper.Instance.BeginUndo("Duplicate");
            //    CopyPasteHelper.Instance.CopyFeedback(feedback);
            //    CopyPasteHelper.Instance.PasteFeedbackAsNew(this, feedbackIndex + 1);
            //    UndoHelper.Instance.EndUndo();
            //});
            //menu.AddSeparator("");

            //menu.AddItem(new GUIContent("Expand All"), false, () => FeedbacksSetExpanded(true));
            //menu.AddItem(new GUIContent("Collapse All"), false, () => FeedbacksSetExpanded(false));

            //menu.AddSeparator("");

            //menu.AddItem(new GUIContent("Documentation"), false, () => ShowFeedbackDocumentation(feedback));

            menu.ShowAsContext();
        }

    }
}
