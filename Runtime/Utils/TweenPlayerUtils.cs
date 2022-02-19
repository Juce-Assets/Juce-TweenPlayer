using Juce.TweenComponent.BindableData;
using Juce.TweenComponent.Bindings;
using Juce.TweenComponent.Components;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Juce.TweenComponent.Utils
{
    public static class TweenPlayerUtils
    {
        public static bool TryBindData(
            TweenPlayer tweenPlayer,
            TweenPlayerCache tweenPlayerCache,
            IBindableData bindableData
            )
        {
            if(tweenPlayer == null) 
            {
                UnityEngine.Debug.LogError($"{nameof(TweenPlayerUtils)} is null, " +
                    $"at {nameof(TweenPlayer)} {tweenPlayer.gameObject.name}", tweenPlayer);
                return false;
            }

            if (!tweenPlayer.BindingEnabled)
            {
                UnityEngine.Debug.LogError($"Tried to bind data, but bindings are disabled, at {nameof(TweenPlayer)} " +
                $"{tweenPlayer.gameObject.name}", tweenPlayer);
                return false;
            }

            if(bindableData == null)
            {
                UnityEngine.Debug.LogError($"Bindable data is null, at {nameof(TweenPlayer)} " +
                    $"{tweenPlayer.gameObject.name}", tweenPlayer);
                return false;
            }

            Type bindableDataType = bindableData.GetType();

            bool found = ReflectionUtils.TryGetAttribute(bindableDataType, out BindableDataAttribute attribute);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Bindable data {bindableDataType.Name} " +
                    $"does not have a {nameof(BindableDataAttribute)}, at {nameof(TweenPlayer)} " +
                    $"{tweenPlayer.gameObject.name}", tweenPlayer);
                return false;
            }

            bool bindableDataIdsEqual = string.Equals(tweenPlayer.BindableDataUid, attribute.Uid);

            if (!bindableDataIdsEqual)
            {
                UnityEngine.Debug.LogError($"Bindable data {bindableDataType.Name} with id {attribute.Uid} does " +
                    $"not match referenced bindable data with id {tweenPlayer.BindableDataUid}, at {nameof(TweenPlayer)} " +
                    $"{tweenPlayer.gameObject.name}", tweenPlayer);
                return false;
            }

            if (!tweenPlayerCache.HasBindableData)
            {
                List<FieldInfo> bindableDataFields = ReflectionUtils.GetFields(bindableDataType);
                List<PropertyInfo> bindableDataProperties = ReflectionUtils.GetProperties(bindableDataType);

                tweenPlayerCache.BindableDataFields.Clear();
                tweenPlayerCache.BindableDataProperties.Clear();

                tweenPlayerCache.BindableDataFields.AddRange(bindableDataFields);
                tweenPlayerCache.BindableDataProperties.AddRange(bindableDataProperties);

                tweenPlayerCache.HasBindableData = true;
            }

            foreach (TweenPlayerComponent component in tweenPlayer.Components)
            {
                if(component == null)
                {
                    UnityEngine.Debug.LogError($"Null component while trying to bind data, at {nameof(TweenPlayer)} " +
                    $"{tweenPlayer.gameObject.name}", tweenPlayer);
                    continue;
                }

                if (!component.Enabled)
                {
                    continue;
                }

                 BindComponent(
                    tweenPlayer,
                    tweenPlayerCache,
                    component, 
                    bindableData,
                    tweenPlayerCache.BindableDataFields,
                    tweenPlayerCache.BindableDataProperties
                    );
            }

            return true;
        }

        private static void BindComponent(
            TweenPlayer tweenPlayer,
            TweenPlayerCache tweenPlayerCache,
            TweenPlayerComponent component,
            IBindableData bindableData,
            IReadOnlyList<FieldInfo> bindableFieldsInfo,
            IReadOnlyList<PropertyInfo> bindablePropertiesInfo
            )
        {
            Type componentType = component.GetType();

            bool found = tweenPlayerCache.ComponentFields.TryGetValue(component, out List<FieldInfo> componentFields);

            if (!found)
            {
                componentFields = ReflectionUtils.GetFields(componentType, typeof(Binding));
                tweenPlayerCache.ComponentFields.Add(component, componentFields);
            }

            foreach (FieldInfo componentFieldInfo in componentFields)
            {
                Binding binding = componentFieldInfo.GetValue(component) as Binding;

                if(binding == null)
                {
                    continue;
                }

                if (!binding.WantsToBeBinded)
                {
                    continue;
                }

                foreach (FieldInfo fields in bindableFieldsInfo)
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

                foreach (PropertyInfo property in bindablePropertiesInfo)
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

                UnityEngine.Debug.LogError($"Field {componentFieldInfo.Name} could " +
                    $"not be binded to '{binding.BindedVariableName}', at {nameof(TweenPlayer)} " +
                    $"{tweenPlayer.gameObject.name}", tweenPlayer);
            }
        }
    }
}
