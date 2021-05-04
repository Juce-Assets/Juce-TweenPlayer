using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Utils
{
    public static class SerializedPropertyUtils
    {
        public static void DrawSerializedPropertyChildren(SerializedProperty serializedProperty, bool showNames)
        {
            if(serializedProperty == null)
            {
                return;
            }

            int startingDepth = serializedProperty.depth;

            SerializedProperty copySerializedProperty = serializedProperty.Copy();

            // Move into the first child of aProp
            bool next =  copySerializedProperty.NextVisible(true);

            while(next && copySerializedProperty.depth > startingDepth)
            {
                if (showNames)
                {
                    EditorGUILayout.PropertyField(copySerializedProperty, true);
                }
                else
                {
                    EditorGUILayout.PropertyField(copySerializedProperty, new GUIContent(""), true, GUILayout.ExpandWidth(false));
                }

                next = copySerializedProperty.NextVisible(false);
            }
        }
	}
}
