using Juce.TweenPlayer.BindableData;

namespace Juce.TweenPlayer.Logic
{
    public static class TryGetEditorBindableDataLogic
    {
        public static bool Execute(
            TweenPlayerEditor bindingPlayerEditor,
            string bindableDataUid,
            out EditorBindableData editorBindableData
            )
        {
            for (int i = 0; i < bindingPlayerEditor.ToolData.EditorBindableDatas.Count; ++i)
            {
                editorBindableData = bindingPlayerEditor.ToolData.EditorBindableDatas[i];

                if (string.Equals(bindableDataUid, editorBindableData.Uid))
                {
                    return true;
                }
            }

            editorBindableData = default;
            return false;
        }
    }
}
