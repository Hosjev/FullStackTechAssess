public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllActiveAsync();
    Task<CategorySummaryDto?> GetSummaryAsync(Guid id);
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
    Task<bool> SoftDeleteAsync(Guid id);
}
