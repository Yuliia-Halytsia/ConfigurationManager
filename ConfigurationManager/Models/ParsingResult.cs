namespace ConfigurationManager.Models
{
    public class SuccessParsingResult : SuccessResult
    {
        public SuccessParsingResult(ConfigurationProperty configurationProperty) : base(configurationProperty)
        {
        }
    }

    public class FailedParsingResult : FailedResult
    {
        public FailedParsingResult(string errorMessage) : base(ErrorType.Parsing, errorMessage)
        {
        }
    }
}
