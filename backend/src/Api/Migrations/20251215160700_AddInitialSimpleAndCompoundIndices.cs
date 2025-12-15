using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialSimpleAndCompoundIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // products: active + id
            migrationBuilder.CreateIndex(
                name: "idx_products_active_id",
                table: "products",
                columns: new[] { "is_active", "id" });

            // products: category + active
            migrationBuilder.CreateIndex(
                name: "idx_products_category_active",
                table: "products",
                columns: new[] { "category_id", "is_active" });

            // categories: active
            migrationBuilder.CreateIndex(
                name: "idx_categories_active",
                table: "categories",
                column: "is_active");

            // products: partial index for huge tables (active only)
            migrationBuilder.Sql("""
                create index idx_products_category_active_only
                on products (category_id)
                where is_active = true;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropIndex(
        name: "idx_products_active_id",
        table: "products");

    migrationBuilder.DropIndex(
        name: "idx_products_category_active",
        table: "products");

    migrationBuilder.DropIndex(
        name: "idx_categories_active",
        table: "categories");

    migrationBuilder.Sql("""
        drop index if exists idx_products_category_active_only;
    """);
}
    }
}
