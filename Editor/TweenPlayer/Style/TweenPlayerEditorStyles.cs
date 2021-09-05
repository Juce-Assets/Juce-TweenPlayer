using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer
{
    public static class TweenPlayerEditorStyles
    {
        private static readonly Color splitterDark = new Color(0.12f, 0.12f, 0.12f, 1.333f);
        private static readonly Color splitterLight = new Color(0.6f, 0.6f, 0.6f, 1.333f);
         
        private static readonly Color headerBackgroundDark = new Color(0.1f, 0.1f, 0.1f, 0.2f);
        private static readonly Color headerBackgroundLight = new Color(1f, 1f, 1f, 0.4f);
         
        private static readonly Color reorderDark = new Color(1f, 1f, 1f, 0.2f);
        private static readonly Color reorderLight = new Color(0.1f, 0.1f, 0.1f, 0.2f);
         
        private static readonly Color reorderRectDark = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        private static readonly Color reorderRectLight = new Color(0.2f, 0.2f, 0.2f, 0.5f);

        public static Color SplitterColor => EditorGUIUtility.isProSkin ? splitterDark : splitterLight; 
        public static Color HeaderBackgroundColor => EditorGUIUtility.isProSkin ? headerBackgroundDark : headerBackgroundLight;
        public static Color ReorderColor => EditorGUIUtility.isProSkin ? reorderDark : reorderLight; 
        public static Color ReorderRectColor => EditorGUIUtility.isProSkin ? reorderRectDark : reorderRectLight; 

        public static Color TaskDelayColor { get; } = new Color(0.8f, 0.3f, 0.3f);
        public static Color TaskRunningColor { get; } = new Color(0.3f, 0.9f, 0.5f);
        public static Color TaskFinishedColor { get; } = new Color(0.9f, 0.9f, 0.9f);
    }
}
