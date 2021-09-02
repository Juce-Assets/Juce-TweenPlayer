using Juce.TweenPlayer.ReflectionComponents;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.PropertyDrawers.ReflectionComponents
{
    [CustomPropertyDrawer(typeof(ReflectionComponentColor))]
    public class ReflectionComponentColorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReflectionComponentDrawer.Draw(position, property, label, typeof(Color));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return ReflectionComponentDrawer.GetPropertyHeight(this, property, label);
        }
    }
}