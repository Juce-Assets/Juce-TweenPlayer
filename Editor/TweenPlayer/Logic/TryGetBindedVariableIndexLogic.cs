using Juce.TweenComponent.Bindings;

namespace Juce.TweenComponent.Logic
{
    public static class TryGetBindedVariableIndexLogic
    {
        public static bool Execute(EditorBinding editorBinding, out int index)
        {
            for (int i = 0; i < editorBinding.BindableFields.Length; ++i)
            {
                if (string.Equals(editorBinding.Binding.BindedVariableName, editorBinding.BindableFields[i]))
                {
                    index = i;
                    return true;
                }
            }

            index = 0;
            return false;
        }
    }
}
