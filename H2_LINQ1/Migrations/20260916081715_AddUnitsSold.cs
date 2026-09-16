using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2_LINQ1.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitsSold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitsSold",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsSold",
                table: "Items");
        }
    }
}
