using Juce.TweenComponent.Validation;
using System.Linq;
using UnityEditor;

namespace Juce.TweenComponent.Drawers
{
    public static class ValidationDrawer
    {
        public static void Draw(ValidationResult validationResult)
        {
            bool needsToShowValidation =
                validationResult.ValidationLogs.Count != 0
                || validationResult.ValidationResultType != ValidationResultType.Success;

            if (!needsToShowValidation)
            {
                return;
            }

            IOrderedEnumerable<ValidationLog> validationLogs = validationResult.ValidationLogs.OrderBy(i => i.LogType);

            foreach (ValidationLog validationLog in validationLogs)
            {
                switch (validationLog.LogType)
                {
                    case ValidationLogType.Info:
                        {
                            EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Info);
                        }
                        break;

                    case ValidationLogType.Warning:
                        {
                            EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Warning);
                        }
                        break;

                    case ValidationLogType.Error:
                        {
                            EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Error);
                        }
                        break;
                }
            }
        }
    }
}
