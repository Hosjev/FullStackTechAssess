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

    // GET /api/categories
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll()
    {
        return Ok(await _categories.GetAllActiveAsync());
    }

    // POST /api/categories
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
    {
        var result = await _categories.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    // DELETE /api/categories/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _categories.SoftDeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // GET /api/categories/{id}/summary
    [HttpGet("{id}/summary")]
    public async Task<ActionResult<CategorySummaryDto>> Summary(Guid id)
    {
        var result = await _categories.GetSummaryAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
