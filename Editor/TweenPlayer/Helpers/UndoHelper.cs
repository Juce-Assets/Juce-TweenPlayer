using UnityEditor;

namespace Juce.TweenComponent.Helpers
{
    public static class UndoHelper 
    {
        private static int groupId;

        public static void BeginUndo()
        {
            Undo.IncrementCurrentGroup();

            groupId = Undo.GetCurrentGroup();
        }

        public static void EndUndo()
        {
            Undo.CollapseUndoOperations(groupId);
        }
    }
}
