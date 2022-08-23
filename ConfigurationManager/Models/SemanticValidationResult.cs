namespace ConfigurationManager.Models
{
    public class SuccessSemanticValidationResult : SuccessResult
    {
        public SuccessSemanticValidationResult(ConfigurationProperty configurationProperty) : base(configurationProperty)
        {
        }
    }

    public class FailedSemanticValidationResult : FailedResult
    {
        public FailedSemanticValidationResult(string errorMessage) : base(ErrorType.Semantic, errorMessage)
        {
        }
    }
}
