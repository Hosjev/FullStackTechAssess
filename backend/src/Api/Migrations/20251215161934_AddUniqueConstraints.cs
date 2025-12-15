using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateIndex(
        name: "ux_categories_name",
        table: "categories",
        column: "name",
        unique: true);

    migrationBuilder.CreateIndex(
        name: "ux_products_category_name",
        table: "products",
        columns: new[] { "category_id", "name" },
        unique: true);
}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropIndex(
        name: "ux_categories_name",
        table: "categories");

    migrationBuilder.DropIndex(
        name: "ux_products_category_name",
        table: "products");
}
    }
}
