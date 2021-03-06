namespace Juce.TweenComponent.Logic
{
    public static class GatherSerializedPropertiesLogic
    {
        public static void Execute(TweenPlayerEditor editor)
        {
            editor.SerializedPropertiesData.ShowProgressBarsProperty = editor.serializedObject.FindProperty("showProgressBars");
            editor.SerializedPropertiesData.ExecutionModeProperty = editor.serializedObject.FindProperty("executionMode");
            editor.SerializedPropertiesData.LoopModeProperty = editor.serializedObject.FindProperty("loopMode");
            editor.SerializedPropertiesData.LoopResetModeProperty = editor.serializedObject.FindProperty("loopResetMode");
            editor.SerializedPropertiesData.LoopsProperty = editor.serializedObject.FindProperty("loops");
            editor.SerializedPropertiesData.BindingsProperty = editor.serializedObject.FindProperty("Bindings");
            editor.SerializedPropertiesData.ComponentsProperty = editor.serializedObject.FindProperty("Components");
            editor.SerializedPropertiesData.BindingEnabledProperty = editor.serializedObject.FindProperty("bindingEnabled");
            editor.SerializedPropertiesData.BindableDataUidProperty = editor.serializedObject.FindProperty("bindableDataUid");
        }
    }
}
