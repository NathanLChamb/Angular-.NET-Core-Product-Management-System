namespace eCommerce.Application.Features.Products.Interfaces
{
    public interface IProductValidationService
    {
        Task ValidateCategoriesAsync(List<int> categoryIds, CancellationToken ct);
        Task ValidateOptionsAsync(List<int> optionIds, CancellationToken ct);
        Task ValidateOptionValuesAsync(List<int> optionValueIds, CancellationToken ct);
        Task ValidateOptionValuesBelongToOptionsAsync(List<int> optionIds, List<int> optionValueId, CancellationToken ct);
    }
}
