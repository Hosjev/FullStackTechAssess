using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _db;

    public CategoryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync()
    {
        return await _db.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<CategorySummaryDto?> GetCategorySummaryAsync(Guid categoryId)
    {
        return await _db.Categories
            .AsNoTracking()
            .Where(c => c.Id == categoryId)
            .Select(c => new CategorySummaryDto
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                ProductCount = c.Products.Count,
                AveragePrice = c.Products.Average(p => p.Price),
                MinPrice = c.Products.Min(p => p.Price),
                MaxPrice = c.Products.Max(p => p.Price)
            })
            .FirstOrDefaultAsync();
    }

}
