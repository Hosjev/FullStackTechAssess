public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync();
    Task<CategorySummaryDto?> GetCategorySummaryAsync(Guid categoryId);
}
