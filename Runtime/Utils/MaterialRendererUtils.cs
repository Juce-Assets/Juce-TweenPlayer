using Juce.TweenComponent.Validation;
using UnityEngine;

namespace Juce.TweenComponent.Utils
{
    public static class MaterialRendererUtils
    {
#if UNITY_EDITOR

        public static void Validate(
            Renderer renderer,
            int materialIndex,
            string materialProperty,
            UnityEditor.ShaderUtil.ShaderPropertyType propertyType,
            ValidationBuilder validationBuilder
            )
        {
            if (renderer.sharedMaterials.Length <= materialIndex)
            {
                validationBuilder.LogError($"Material index does not exist");
                validationBuilder.SetError();
                return;
            }

            Material material = renderer.sharedMaterials[materialIndex];

            if (material == null)
            {
                validationBuilder.LogError($"Null material");
                validationBuilder.SetError();
                return;
            }

            bool propertyExists = material.HasProperty(materialProperty);

            if (!propertyExists)
            {
                validationBuilder.LogError($"Property '{materialProperty}' does not exist");
                return;
            }

            int propertyIndex = material.shader.FindPropertyIndex(materialProperty);

            UnityEditor.ShaderUtil.ShaderPropertyType type = UnityEditor.ShaderUtil.GetPropertyType(
                material.shader,
                propertyIndex
                );

            if (type != propertyType)
            {
                validationBuilder.LogError($"Property {materialProperty} is of type {type}, but " +
                    $"we were expecting {propertyType}");
                validationBuilder.SetError();
            }
        }

#endif

        public static bool PropertyExists(
            Renderer renderer,
            int materialIndex,
            string materialProperty
            )
        {
            if (renderer.materials.Length <= materialIndex)
            {
                return false;
            }

            Material material = renderer.materials[materialIndex];

            if (material == null)
            {
                return false;
            }

            bool propertyExists = material.HasProperty(materialProperty);

            if (!propertyExists)
            {
                return false;
            }

            return true;
        }
    }
}
