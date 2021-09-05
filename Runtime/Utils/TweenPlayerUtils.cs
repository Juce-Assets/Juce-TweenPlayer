using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Bindings;
using Juce.TweenPlayer.Components;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Juce.TweenPlayer.Utils
{
    public static class TweenPlayerUtils
    {
        public static bool TryBindData(
            TweenPlayer tweenPlayer,
            IBindableData bindableData
            )
        {
            if(tweenPlayer == null) 
            {
                UnityEngine.Debug.LogError($"{nameof(TweenPlayerUtils)} is null", tweenPlayer);
                return false;
            }

            if (!tweenPlayer.BindingEnabled)
            {
                return false;
            }

            if(bindableData == null)
            {
                UnityEngine.Debug.LogError($"Bindable data is null", tweenPlayer);
                return false;
            }

            Type bindableDataType = bindableData.GetType();

            bool found = ReflectionUtils.TryGetAttribute(bindableDataType, out BindableDataAttribute attribute);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Bindable data {bindableData.GetType().Name} " +
                    $"does not have a {nameof(BindableDataAttribute)}", tweenPlayer);
                return false;
            }

            if (!string.Equals(tweenPlayer.BindableDataUid, attribute.Uid))
            {
                UnityEngine.Debug.LogError($"Bindable data {bindableData.GetType().FullName} does " +
                    $"not match referenced bindable data {bindableData}", tweenPlayer);
                return false;
            }

            List<FieldInfo> bindableDataFields = ReflectionUtils.GetFields(bindableDataType);
            List<PropertyInfo> bindableDataProperties = ReflectionUtils.GetProperties(bindableDataType);

            foreach (TweenPlayerComponent component in tweenPlayer.Components)
            {
                if(component == null)
                {
                    UnityEngine.Debug.LogError($"Null component while trying to bind data");
                    continue;
                }

                if (!component.Enabled)
                {
                    continue;
                }

                 BindComponent(
                    tweenPlayer, 
                    component, 
                    bindableData,
                    bindableDataFields,
                    bindableDataProperties
                    );
            }

            return true;
        }

        private static void BindComponent(
            TweenPlayer tweenPlayer,
            TweenPlayerComponent component,
            IBindableData bindableData,
            IReadOnlyList<FieldInfo> bindableDataFields,
            IReadOnlyList<PropertyInfo> bindableDataProperties
            )
        {
            List<FieldInfo> thisFields = ReflectionUtils.GetFields(component.GetType(), typeof(Binding));

            foreach (FieldInfo fieldInfo in thisFields)
            {
                Binding binding = (Binding)fieldInfo.GetValue(component);

                if (!binding.WantsToBeBinded)
                {
                    continue;
                }

                foreach (FieldInfo fields in bindableDataFields)
                {
                    if (binding.BindingType != fields.FieldType)
                    {
                        continue;
                    }

                    if (!string.Equals(fields.Name, binding.BindedVariableName))
                    {
                        continue;
                    }

                    object obj = fields.GetValue(bindableData);

                    binding.SetBindedValue(obj);

                    return;
                }

                foreach (PropertyInfo property in bindableDataProperties)
                {
                    if (binding.BindingType != property.PropertyType)
                    {
                        continue;
                    }

                    if (!string.Equals(property.Name, binding.BindedVariableName))
                    {
                        continue;
                    }

                    object obj = property.GetValue(bindableData);

                    binding.SetBindedValue(obj);

                    return;
                }

                UnityEngine.Debug.LogError($"Field {fieldInfo.Name} could not be binded to '{binding.BindedVariableName}'", tweenPlayer);
            }
        }
    }
}
