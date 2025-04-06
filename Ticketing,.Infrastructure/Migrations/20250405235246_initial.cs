using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing_.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ticketingInventory",
                columns: table => new
                {
                    EventTicketInventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventId = table.Column<int>(type: "int", nullable: false),
                    eventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticketTier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pricePerTicket = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    available = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketingInventory", x => x.EventTicketInventoryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticketingInventory");
        }
    }
}
