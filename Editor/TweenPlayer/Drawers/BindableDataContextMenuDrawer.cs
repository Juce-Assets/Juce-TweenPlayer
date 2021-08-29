using Juce.TweenPlayer.BindableData;
using UnityEditor;
using UnityEngine;
using static UnityEditor.GenericMenu;

namespace Juce.TweenPlayer
{
    public static class BindableDataContextMenuDrawer
    {
        public static void Draw(
            TweenPlayerEditor editor
            )
        {

            GenericMenu menu = new GenericMenu();

            foreach (EditorBindableData bindableDatas in editor.EditorBindableDatas)
            {
                MenuFunction selectedAction = () =>
                {
                    editor.BindableDataUidProperty.stringValue = bindableDatas.Uid;
                    editor.serializedObject.ApplyModifiedProperties();
                };

                menu.AddItem(
                    new GUIContent($"{bindableDatas.MenuPath}"),
                    false, 
                    selectedAction
                    );
            }

            menu.ShowAsContext();
        }
    }
}
