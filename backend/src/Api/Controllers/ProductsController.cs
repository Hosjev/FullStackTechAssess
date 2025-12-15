using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _products;

    public ProductsController(IProductService products)
    {
        _products = products;
    }

    // GET /api/products
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll()
    {
        var result = await _products.GetAllActiveAsync();
        return Ok(result);
    }

    // GET /api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var result = await _products.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _products.SoftDeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
