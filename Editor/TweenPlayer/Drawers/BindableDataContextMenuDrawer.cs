using Juce.TweenPlayer.BindableData;
using UnityEditor;
using UnityEngine;
using static UnityEditor.GenericMenu;

namespace Juce.TweenPlayer.Drawers
{
    public static class BindableDataContextMenuDrawer
    {
        public static void Draw(
            TweenPlayerEditor editor
            )
        {

            GenericMenu menu = new GenericMenu();

            foreach (EditorBindableData bindableDatas in editor.ToolData.EditorBindableDatas)
            {
                MenuFunction selectedAction = () =>
                {
                    editor.SerializedPropertiesData.BindableDataUidProperty.stringValue = bindableDatas.Uid;
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
