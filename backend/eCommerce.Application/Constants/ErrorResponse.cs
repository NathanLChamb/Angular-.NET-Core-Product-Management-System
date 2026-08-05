namespace eCommerce.Application.Constants
{
    public record ErrorResponse(
        string Code,
        string Message,
        DateTime Timestamp,
        string RequestId
    );
}
