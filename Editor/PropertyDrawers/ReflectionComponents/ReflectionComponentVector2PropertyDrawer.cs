using Juce.TweenPlayer.ReflectionComponents;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.PropertyDrawers.ReflectionComponents
{
    [CustomPropertyDrawer(typeof(ReflectionComponentVector2))]
    public class ReflectionComponentVector2PropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReflectionComponentDrawer.Draw(position, property, label, typeof(Vector2));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return ReflectionComponentDrawer.GetPropertyHeight(this, property, label);
        }
    }
}