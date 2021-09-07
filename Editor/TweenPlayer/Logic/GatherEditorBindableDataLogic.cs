using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Juce.TweenPlayer.Logic
{
    public static class GatherEditorBindableDataLogic
    {
        public static void Execute(TweenPlayerEditor editor)
        {
            editor.ToolData.EditorBindableDatas.Clear();

            List<Type> types = ReflectionUtils.GetInheritedTypes(typeof(IBindableData));

            foreach (Type type in types)
            {
                bool found = ReflectionUtils.TryGetAttribute(type, out BindableDataAttribute attribute);

                if (!found)
                {
                    continue;
                }

                List<EditorBindableDataField> editorBindableDataFields = new List<EditorBindableDataField>();
                List<EditorBindableDataProperty> editorBindableDataProperties = new List<EditorBindableDataProperty>();

                FieldInfo[] fields = type.GetFields(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance
                    );

                PropertyInfo[] properties = type.GetProperties();

                foreach (FieldInfo field in fields)
                {
                    editorBindableDataFields.Add(new EditorBindableDataField(field.FieldType, field.Name));
                }

                foreach (PropertyInfo property in properties)
                {
                    editorBindableDataProperties.Add(new EditorBindableDataProperty(property.PropertyType, property.Name));
                }

                string[] fieldsNames = editorBindableDataFields.Select(i => i.Name).ToArray();

                editor.ToolData.EditorBindableDatas.Add(new EditorBindableData(
                    type,
                    attribute.Name,
                    attribute.MenuPath,
                    attribute.Uid,
                    editorBindableDataFields,
                    editorBindableDataProperties
                    ));
            }
        }
    }
}
