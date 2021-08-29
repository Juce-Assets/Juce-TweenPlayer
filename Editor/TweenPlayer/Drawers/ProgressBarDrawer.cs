using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Drawers
{
    public static class ProgressBarDrawer
    {
        public static void Draw(
            float normalizedProgress,
            Color color,
            float offsetX = 0,
            float offsetY = 0,
            float height = 2
            )
        {
            Rect progressRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            progressRect.x -= 3 - offsetX;
            progressRect.y += offsetY;
            progressRect.width += 6 - (offsetX * 2);
            progressRect.height = height;

            progressRect.width *= normalizedProgress;

            EditorGUI.DrawRect(progressRect, color);
        }
    }
}
