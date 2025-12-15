using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categories;

    public CategoriesController(ICategoryService categories)
    {
        _categories = categories;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
    {
        var result = await _categories.GetCategoriesAsync();
        return Ok(result);
    }

    [HttpGet("{id}/summary")]
    public async Task<ActionResult<CategorySummaryDto>> GetCategorySummary(Guid id)
    {
        var result = await _categories.GetCategorySummaryAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

}
