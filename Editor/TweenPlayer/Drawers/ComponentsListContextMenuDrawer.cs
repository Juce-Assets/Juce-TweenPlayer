using Juce.TweenPlayer.Logic;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class ComponentsListContextMenuDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            GenericMenu menu = new GenericMenu();

            foreach (EditorTweenPlayerComponent component in editor.ToolData.EditorPlayerComponents)
            {
                menu.AddItem(new GUIContent(
                    $"{component.MenuPath}"),
                    false, 
                    () => CreateComponentLogic.Execute(editor, component.Type)
                    );
            }

            menu.ShowAsContext();
        }
    }
}
