namespace Juce.TweenComponent.Validation
{
    public class ValidationLog
    {
        public ValidationLogType LogType { get; }
        public string LogMessage { get; }

        public ValidationLog(ValidationLogType logType, string logMessage)
        {
            LogType = logType;
            LogMessage = logMessage;
        }
    }
}
