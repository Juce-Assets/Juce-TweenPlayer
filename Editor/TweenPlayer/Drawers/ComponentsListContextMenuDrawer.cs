using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class ComponentsListContextMenuDrawer
    {
        public static void Draw(TweenPlayerEditor editor)
        {
            GenericMenu menu = new GenericMenu();

            foreach (EditorTweenPlayerComponent component in editor.EditorPlayerComponents)
            {
                menu.AddItem(new GUIContent($"{component.MenuPath}"),
                false, () => editor.ActualTarget.AddComponent(component.Type));
            }

            menu.ShowAsContext();
        }
    }
}
