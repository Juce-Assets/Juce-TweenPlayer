using UnityEditor;
using UnityEngine;

namespace Juce.TweenPlayer.Helpers
{
    public class DragHelper
    {
        private int draggedStartId = -1;
        private int draggedEndId = -1;

        public void CheckDraggingItem(
            Event e, 
            Rect interactionRect,
            Rect secondaryInteractionRect,
            Color color, 
            int index
            )
        {
            if (e.type == EventType.MouseDown)
            {
                if (interactionRect.Contains(e.mousePosition))
                {
                    draggedStartId = index;
                    e.Use();
                }
            }

            // Draw rect if feedback is being dragged
            if (draggedStartId == index && interactionRect != Rect.zero)
            {
                EditorGUI.DrawRect(secondaryInteractionRect, color);
            }

            // If hovering at the top while dragging one, check where
            // it should be dropped: top or bottom
            bool rectContainsMousePosition = secondaryInteractionRect.Contains(e.mousePosition);

            if (!rectContainsMousePosition || draggedStartId < 0)
            {
                return;
            }

            bool needsToChangeIndex = secondaryInteractionRect.Contains(e.mousePosition);

            if (!needsToChangeIndex)
            {
                return;
            }

            draggedEndId = index;
        }

        public bool ResolveDragging(Event e, out int startIndex, out int endIndex)
        {
            bool ret = false;

            startIndex = -1;
            endIndex = -1;

            if (draggedStartId >= 0 && draggedEndId >= 0)
            {
                if (draggedEndId != draggedStartId)
                {
                    startIndex = draggedStartId;
                    endIndex = draggedEndId;

                    draggedStartId = draggedEndId;

                    ret = true;
                }
            }

            if (draggedStartId >= 0 || draggedEndId >= 0)
            {
                if (e.type == EventType.MouseUp)
                {
                    draggedStartId = -1;
                    draggedEndId = -1;
                    e.Use();
                }
            }

            return ret;
        }
    }
}