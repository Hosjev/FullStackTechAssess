using System.ComponentModel.DataAnnotations;

public class CreateProductDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}
