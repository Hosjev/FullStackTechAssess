public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public CategoryDto Category { get; set; } = null!;
}
