using UnityEditor;

namespace Juce.TweenPlayer.Helpers
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
