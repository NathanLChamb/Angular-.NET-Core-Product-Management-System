namespace eCommerce.Application.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public string Code { get; }
        public BusinessRuleException(string message, string code = "BUSINESS_RULE_ERROR") : base(message)
        {
            Code = code;
        }
    }
}
