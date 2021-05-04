using System.Collections.Generic;

namespace Juce.TweenPlayer.Validation
{
    public class ValidationResult
    {
        public static ValidationResult EmptySuccess => new ValidationResult(ValidationResultType.Success, new List<ValidationLog>());

        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<ValidationLog> ValidationLogs { get; }

        public ValidationResult(ValidationResultType validationResultType, IReadOnlyList<ValidationLog> validationLogs)
        {
            ValidationResultType = validationResultType;
            ValidationLogs = validationLogs;
        }
    }
}
