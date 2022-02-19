using Juce.TweenComponent.Bindings;
using System;
using System.Linq;

namespace Juce.TweenComponent.Logic
{
    public static class UpdateBindingDataLogic
    {
        public static void Execute(
            TweenPlayerEditor bindingPlayerEditor,
            EditorBinding editorBinding
            )
        {
            editorBinding.BindableFields = Array.Empty<string>();

            editorBinding.Binding.BindingEnabled = bindingPlayerEditor.SerializedPropertiesData.BindingEnabledProperty.boolValue;

            bool bindingEnabled = editorBinding.Binding.WantsToBeBinded 
                && bindingPlayerEditor.SerializedPropertiesData.BindingEnabledProperty.boolValue;

            if (!bindingEnabled)
            {
                editorBinding.Binding.Binded = false;
            }
            else
            {
                bool hasBindableData = bindingPlayerEditor.ToolData.SelectedEditorBindableData != null;

                if (!hasBindableData)
                {
                    editorBinding.Binding.Binded = false;

                    return;
                }

                string[] fieldsNames = bindingPlayerEditor.ToolData.SelectedEditorBindableData.Fields.
                       Where(i => editorBinding.Type.IsAssignableFrom(i.Type)).Select(i => i.Name).ToArray();

                string[] propertiesNames = bindingPlayerEditor.ToolData.SelectedEditorBindableData.Properties.
                    Where(i => editorBinding.Type.IsAssignableFrom(i.Type)).Select(i => i.Name).ToArray();

                editorBinding.BindableFields = new string[fieldsNames.Length + propertiesNames.Length];
                Array.Copy(fieldsNames, editorBinding.BindableFields, fieldsNames.Length);
                Array.Copy(propertiesNames, 0, editorBinding.BindableFields, fieldsNames.Length, propertiesNames.Length);

                bool hasBindableProperties = editorBinding.BindableFields.Length > 0;

                if (!hasBindableProperties)
                {
                    editorBinding.Binding.Binded = false;

                    return;
                }

                bool found = TryGetBindedVariableIndexLogic.Execute(editorBinding, out int index);

                if (found)
                {
                    editorBinding.Binding.BindedVariableName = editorBinding.BindableFields[index];
                }

                editorBinding.Binding.Binded = found;
            }
        }
    }
}
