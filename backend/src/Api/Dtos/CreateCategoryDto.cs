using System.ComponentModel.DataAnnotations;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;
}
