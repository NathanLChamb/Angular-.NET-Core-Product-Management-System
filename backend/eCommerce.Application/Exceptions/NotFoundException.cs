namespace eCommerce.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Code { get; }
        public NotFoundException(string message, string code = "NOT_FOUND_ERROR") : base(message) 
        {
            Code = code;
        }
    }
}
