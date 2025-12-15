using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllActiveAsync()
    {
        return await _db.Products
            .AsNoTracking()
            .Where(p => p.IsActive)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        return await _db.Products
            .AsNoTracking()
            .Where(p => p.Id == id && p.IsActive)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
            .FirstOrDefaultAsync();
    }
    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var product = await _db.Products
            .Where(p => p.Id == id && p.IsActive)
            .FirstOrDefaultAsync();

        if (product == null)
            return false;

        product.IsActive = false;
        await _db.SaveChangesAsync();
        return true;
    }

}
