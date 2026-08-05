namespace eCommerce.Application.Exceptions
{
    public class ValidationRuleException : Exception
    {
        public string Code { get; }
        public ValidationRuleException(string message, string code = "VALIDATION_RULE_ERROR") : base(message) 
        {
            Code = code;
        }
    }
}
