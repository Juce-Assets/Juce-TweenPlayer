using Juce.TweenPlayer.Bindings;

namespace Juce.TweenPlayer.Utils
{
    public static class BindingUtils
    { 
        public static void TrySetBindedValue<T>(object objectValue, ref T bindedValue)
        {
            if (objectValue == null)
            {
                UnityEngine.Debug.LogError("Object value was null");
                bindedValue = default;
                return;
            }

            bool canBeUsed = typeof(T).IsAssignableFrom(objectValue.GetType());

            if (!canBeUsed)
            {
                UnityEngine.Debug.LogError($"Object value is not assignable to {typeof(T).Name}");
                bindedValue = default;
                return;
            }

            bindedValue = (T)objectValue;
        }

        public static T TryGetValue<T>(Binding binding, T bindedValue, T fallbackValue)
        {
            if (binding.BindingEnabled && binding.WantsToBeBinded && binding.Binded)
            {
                return bindedValue;
            }

            if(binding.BindingEnabled && binding.WantsToBeBinded && !binding.Binded)
            {
                UnityEngine.Debug.LogError($"Object binding not valid of type {typeof(T).Name}, using default");
                return default;
            }

            return fallbackValue;
        }

        public static string ToString<T>(Binding binding, T fallbackValue)
        {
            if (binding.BindingEnabled && binding.WantsToBeBinded && binding.Binded)
            {
                return $"Binded to {binding.BindedVariableName}";
            }

            if (binding.BindingEnabled && binding.WantsToBeBinded && !binding.Binded)
            {
                return "Missing binding";
            }

            if(fallbackValue == null)
            {
                return string.Empty;
            }

            UnityEngine.Object fallbackValueObject = fallbackValue as UnityEngine.Object;

            if (fallbackValueObject != null)
            {
                return fallbackValueObject.name;
            }

            return fallbackValue.ToString();
        }
    }
}
