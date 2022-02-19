using System.Collections.Generic;

namespace Juce.TweenComponent.Validation
{
    public class ValidationBuilder
    {
        private ValidationResultType validationResultType = ValidationResultType.Success;
        private readonly List<ValidationLog> validationLogs = new List<ValidationLog>();

        public ValidationBuilder()
        {

        }

        public ValidationBuilder(ValidationResult nestedResult)
        {
            if(nestedResult == null)
            {
                return;
            }

            if(nestedResult.ValidationResultType == ValidationResultType.Success)
            {
                return;
            }

            validationResultType = nestedResult.ValidationResultType;

            validationLogs.AddRange(nestedResult.ValidationLogs);
        }

        public void SetError()
        {
            validationResultType = ValidationResultType.Error;
        }

        public void LogError(string logMessage)
        {
            validationLogs.Add(new ValidationLog(ValidationLogType.Error, logMessage));
        }

        public void LogWarning(string logMessage)
        {
            validationLogs.Add(new ValidationLog(ValidationLogType.Warning, logMessage));
        }

        public void LogInfo(string logMessage)
        {
            validationLogs.Add(new ValidationLog(ValidationLogType.Info, logMessage));
        }

        public ValidationResult Build()
        {
            return new ValidationResult(validationResultType, validationLogs);
        }
    }
}
