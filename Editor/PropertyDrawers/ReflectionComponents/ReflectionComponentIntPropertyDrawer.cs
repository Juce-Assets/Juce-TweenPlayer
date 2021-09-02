using Juce.TweenPlayer.ReflectionComponents;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.PropertyDrawers.ReflectionComponents
{
    [CustomPropertyDrawer(typeof(ReflectionComponentInt))]
    public class ReflectionComponentIntPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReflectionComponentDrawer.Draw(position, property, label, typeof(int));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return ReflectionComponentDrawer.GetPropertyHeight(this, property, label);
        }
    }
}