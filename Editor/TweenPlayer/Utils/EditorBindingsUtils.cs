using Juce.TweenComponent.Bindings;
using System.Collections.Generic;
using System.Reflection;

namespace Juce.TweenComponent.Utils
{
    public static class EditorBindingsUtils
    {
        public static List<EditorBinding> GatherEditorBindings(object targetObject)
        {
            List<EditorBinding> ret = new List<EditorBinding>();

            List<FieldInfo> fields = ReflectionUtils.GetFields(targetObject.GetType(), typeof(Binding));

            foreach (FieldInfo field in fields)
            {
                Binding bindingInstance = (Binding)field.GetValue(targetObject);

                ret.Add(new EditorBinding(
                    bindingInstance.BindingType,
                    field.Name,
                    bindingInstance
                    ));
            }

            return ret;
        }
    }
}
