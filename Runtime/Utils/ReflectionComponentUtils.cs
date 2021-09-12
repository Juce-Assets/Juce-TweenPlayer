using System;
using System.Collections.Generic;
using System.Reflection;

namespace Juce.TweenPlayer.Utils
{
    public static class ReflectionComponentUtils
    {
        public static bool TryFindProperty(
            Type componentType, 
            string propertyName, 
            Type propertyType,
            out PropertyInfo foundPropertyInfo
            )
        {
            List<PropertyInfo> properties = ReflectionUtils.GetProperties(
                componentType
                );

            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.PropertyType == propertyType)
                {
                    if(propertyInfo.Name == propertyName)
                    {
                        foundPropertyInfo = propertyInfo;
                        return true;
                    }
                }
            }

            foundPropertyInfo = default;
            return false;
        }

        public static bool TryFindField(
           Type componentType,
           string fieldName,
           Type fieldType,
           out FieldInfo foundFieldInfo
           )
        {
            List<FieldInfo> fields = ReflectionUtils.GetFields(
                componentType
                );

            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType == fieldType)
                {
                    if (string.Equals(fieldInfo.Name, fieldName))
                    {
                        foundFieldInfo = fieldInfo;
                        return true;
                    }
                }
            }

            foundFieldInfo = default;
            return false;
        }

        public static bool TryFindFieldOrProperty(
           Type componentType,
           string name,
           Type type,
           out FieldInfo foundFieldInfo,
           out PropertyInfo foundPropertyInfo
           )
        {
            bool fieldFound = TryFindField(
                componentType,
                name,
                type,
                out foundFieldInfo
                );

            if(fieldFound)
            {
                foundPropertyInfo = null;
                return true;
            }

            bool propertyFound = TryFindProperty(
                componentType,
                name,
                type,
                out foundPropertyInfo
                );

            if(propertyFound)
            {
                foundFieldInfo = null;
                return true;
            }

            foundPropertyInfo = null;
            foundFieldInfo = null;
            return false;
        }

        public static void SetValue(
            FieldInfo fieldInfo, 
            PropertyInfo propertyInfo,
            object obj,
            object value
            )
        {
            if(fieldInfo != null)
            {
                fieldInfo.SetValue(obj, value);
                return;
            }

            if(propertyInfo != null)
            {
                propertyInfo.SetValue(obj, value);
                return;
            }
        }

        public static T GetValue<T>(
            FieldInfo fieldInfo,
            PropertyInfo propertyInfo,
            object obj
            )
        {
            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(obj);
            }

            if (propertyInfo != null)
            {
                return (T)propertyInfo.GetValue(obj);
            }

            return default(T);
        }
    }
}
