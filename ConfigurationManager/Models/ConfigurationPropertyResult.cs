namespace ConfigurationManager.Models
{
    public abstract class ConfigurationPropertyResult
    {
        public ConfigurationProperty ConfigurationProperty { get; }
        public bool IsValid { get; }
        public ValidationError ValidationError { get; }

        protected ConfigurationPropertyResult(
                                           ConfigurationProperty configurationProperty,
                                           bool isValid,
                                           ValidationError validationError)
        {
            ConfigurationProperty = configurationProperty;
            IsValid = isValid;
            ValidationError = validationError;
        }
    }

    public abstract class SuccessResult : ConfigurationPropertyResult
    {
        protected SuccessResult(ConfigurationProperty configurationProperty) 
            : base(configurationProperty, true,null)
        {
        }
    }

    public abstract class FailedResult : ConfigurationPropertyResult
    {
        protected FailedResult(ErrorType errorType, string errorMessage)
            : base(null, false, new ValidationError(errorType, errorMessage))
        {
        }
    }
    
    public class ValidationError
    {
        public ErrorType Type { get; }
        public string Message { get; }

        public ValidationError(ErrorType type, string message)
        {
            Type = type;
            Message = message;
        }
    }

    public enum ErrorType
    {
        Semantic,
        Parsing
    }
}
