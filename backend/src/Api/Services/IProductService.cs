public interface IProductService
{
    Task<IReadOnlyList<ProductDto>> GetAllActiveAsync();
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<bool> SoftDeleteAsync(Guid id);
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto);
}
