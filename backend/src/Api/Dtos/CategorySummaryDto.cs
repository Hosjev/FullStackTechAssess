public class CategorySummaryDto
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? CategoryDescription { get; set; }

    public int TotalProducts { get; set; }
    public int ActiveProducts { get; set; }

    public decimal AveragePrice { get; set; }
    public decimal TotalInventoryValue { get; set; }

    public PriceRangeDto PriceRange { get; set; } = null!;
    public int OutOfStockCount { get; set; }
}

public class PriceRangeDto
{
    public decimal Min { get; set; }
    public decimal Max { get; set; }
}
