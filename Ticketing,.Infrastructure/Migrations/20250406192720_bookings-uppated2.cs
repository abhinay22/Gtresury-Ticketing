using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing_.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookingsuppated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "totalPrice",
                table: "booking",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "booking");
        }
    }
}
