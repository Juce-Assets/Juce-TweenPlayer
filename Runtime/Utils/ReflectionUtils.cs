using System;
using System.Collections.Generic;
using System.Reflection;

namespace Juce.TweenPlayer.Utils
{
    public static class ReflectionUtils
    {
        public static List<Type> GetInheritedTypes(Type baseType, bool includeAbstractsAndInterfaces = false)
        {
            List<Type> ret = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (!baseType.IsAssignableFrom(type))
                    {
                        continue;
                    }

                    if (baseType == type)
                    {
                        continue;
                    }

                    if (!includeAbstractsAndInterfaces && (type.IsAbstract || type.IsInterface))
                    {
                        continue;
                    }

                    ret.Add(type);
                }
            }

            return ret;
        }

        public static List<FieldInfo> GetFields(Type whereType, bool includeAbstractsAndInterfaces = false)
        {
            List<FieldInfo> ret = new List<FieldInfo>();

            FieldInfo[] fields = whereType.GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetProperty
                );

            foreach (FieldInfo field in fields)
            {
                if (!includeAbstractsAndInterfaces && (field.FieldType.IsAbstract || field.FieldType.IsInterface))
                {
                    continue;
                }

                ret.Add(field);
            }

            return ret;
        }

        public static List<FieldInfo> GetFields(Type whereType, Type fieldType, bool includeAbstractsAndInterfaces = false)
        {
            List<FieldInfo> ret = new List<FieldInfo>();

            FieldInfo[] fields = whereType.GetFields(
                BindingFlags.Public | 
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetProperty
                );

            foreach (FieldInfo field in fields)
            {
                if (!fieldType.IsAssignableFrom(field.FieldType))
                {
                    continue;
                }

                if (!includeAbstractsAndInterfaces && (field.FieldType.IsAbstract || field.FieldType.IsInterface))
                {
                    continue;
                }

                ret.Add(field);
            }

            return ret;
        }

        public static List<PropertyInfo> GetProperties(
            Type whereType, 
            bool includeAbstractsAndInterfaces = false
            )
        {
            List<PropertyInfo> ret = new List<PropertyInfo>();

            PropertyInfo[] properties = whereType.GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetProperty
                );

            foreach (PropertyInfo property in properties)
            {
                if (!includeAbstractsAndInterfaces && (property.PropertyType.IsAbstract || property.PropertyType.IsInterface))
                {
                    continue;
                }

                ret.Add(property);
            }

            return ret;
        }

        public static bool TryGetAttribute<T>(FieldInfo fieldInfo, out T attribute) where T : Attribute
        {
            attribute = fieldInfo.GetCustomAttribute<T>();

            if (attribute == null)
            {
                return false;
            }

            return true;
        }

        public static bool TryGetAttribute<T>(Type baseType, out T attribute) where T : Attribute
        {
            attribute = baseType.GetCustomAttribute<T>();

            if(attribute == null)
            {
                return false;
            }

            return true;
        }
    }
}