using Juce.TweenComponent.ReflectionComponents;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenComponent.PropertyDrawers.ReflectionComponents
{
    [CustomPropertyDrawer(typeof(ReflectionComponentString))]
    public class ReflectionComponentStringPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReflectionComponentDrawer.Draw(position, property, label, typeof(string));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return ReflectionComponentDrawer.GetPropertyHeight(this, property, label);
        }
    }
}