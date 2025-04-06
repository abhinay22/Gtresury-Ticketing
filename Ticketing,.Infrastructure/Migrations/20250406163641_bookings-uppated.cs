using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing_.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookingsuppated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_Status_StatusId",
                table: "booking");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_booking_StatusId",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "booking");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Booking_Status_Valid",
                table: "booking",
                sql: "[Status] IN ('Confirmed', 'Reserved', 'Failed')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Booking_Status_Valid",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "status",
                table: "booking");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_StatusId",
                table: "booking",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_Status_StatusId",
                table: "booking",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
