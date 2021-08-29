using System;
using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class ComponentHeaderDrawer
    {
        public static GUIStyle SmallTickbox { get; } = new GUIStyle("ShurikenToggle");

        public static void Draw(
            string title,
            string extraTitle,
            ref bool enabled,
            ref bool folded,
            Action showGenericMenu,
            out Rect reorderInteractionRect,
            out Rect secondaryInteractionRect
            )
        {
            Color color = Color.white;

            Event e = Event.current;

            Rect backgroundRect = GUILayoutUtility.GetRect(4f, 17f);

            float offset = 28f;

            reorderInteractionRect = backgroundRect;
            reorderInteractionRect.xMin = offset;
            reorderInteractionRect.y += 5f;
            reorderInteractionRect.width = 9f;
            reorderInteractionRect.height = 9f;

            Rect foldoutRect = backgroundRect;
            foldoutRect.y += 2f;
            foldoutRect.xMin = offset + 13f;
            foldoutRect.width = 13f;
            foldoutRect.height = 13f;

            Rect toggleRect = backgroundRect;
            toggleRect.x += 16f;
            toggleRect.xMin = offset + 29f;
            toggleRect.y += 2f;
            toggleRect.width = 13f;
            toggleRect.height = 13f;

            Rect labelRect = backgroundRect;
            labelRect.xMin = offset + 48f;
            labelRect.xMax -= 20f;

            Vector2 textDimensions = EditorStyles.boldLabel.CalcSize(new GUIContent(title));

            Rect extraTitleRect = labelRect;
            extraTitleRect.xMin += textDimensions.x + 5;

            Rect menuRect = new Rect(labelRect.xMax + 4f, labelRect.y - 5f, 16, 20);

            // Background rect should be full width
            backgroundRect.xMin -= 3f;
            backgroundRect.yMin -= 2f;
            backgroundRect.width += 3f;
            backgroundRect.height += 2f;

            // Foldout
            folded = !GUI.Toggle(foldoutRect, !folded, GUIContent.none, EditorStyles.foldout);

            // Title
            using (new EditorGUI.DisabledScope(!enabled))
            {
                EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);
            }

            // Active checkbox
            enabled = GUI.Toggle(toggleRect, enabled, GUIContent.none, SmallTickbox);

            for (int i = 0; i < 3; i++)
            {
                Rect r = reorderInteractionRect;
                r.height = 1;
                r.y = reorderInteractionRect.y + reorderInteractionRect.height * (i / 3.0f);
                EditorGUI.DrawRect(r, TweenPlayerEditorStyles.ReorderColor);
            }

            EditorGUI.LabelField(extraTitleRect, extraTitle);

            // Generic menu
            EditorGUI.LabelField(menuRect, "...", EditorStyles.boldLabel);

            if (e.type == EventType.MouseDown)
            {
                if (menuRect.Contains(e.mousePosition))
                {
                    showGenericMenu?.Invoke();
                    e.Use();
                }
            }

            if (e.type == EventType.MouseDown && labelRect.Contains(e.mousePosition) && e.button == 0)
            {
                folded = !folded;
                e.Use();
            }

            secondaryInteractionRect = backgroundRect;
        }
    }
}
