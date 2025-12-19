using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _db;

    public CategoryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetAllActiveAsync()
    {
        return await _db.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var category = await _db.Categories
            .Where(c => c.Id == id && c.IsActive)
            .FirstOrDefaultAsync();

        if (category == null)
            return false;

        category.IsActive = false;
        category.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }

    // keep existing summary logic, but ensure IsActive is enforced
    public async Task<CategorySummaryDto?> GetSummaryAsync(Guid id)
    {
        return await _db.Categories
            .AsNoTracking()
            .Where(c => c.Id == id && c.IsActive)
            .Select(c => new CategorySummaryDto
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                CategoryDescription = c.Description,

                TotalProducts = c.Products.Count(),
                ActiveProducts = c.Products.Count(p => p.IsActive),

                AveragePrice = Math.Round(
                    c.Products.Where(p => p.IsActive).Average(p => p.Price),
                    2
                ),

                TotalInventoryValue = Math.Round(
                    c.Products.Where(p => p.IsActive)
                        .Sum(p => p.Price * p.StockQuantity),
                    2
                ),

                PriceRange = new PriceRangeDto
                {
                    Min = c.Products
                        .Where(p => p.IsActive)
                        .Min(p => p.Price),

                    Max = c.Products
                        .Where(p => p.IsActive)
                        .Max(p => p.Price)
                },

                OutOfStockCount = c.Products
                    .Count(p => p.IsActive && p.StockQuantity == 0)
            })
            .FirstOrDefaultAsync();
    }
}
