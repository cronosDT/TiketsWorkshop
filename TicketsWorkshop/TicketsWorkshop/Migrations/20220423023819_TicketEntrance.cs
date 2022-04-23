using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsWorkshop.Migrations
{
    public partial class TicketEntrance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketEntrances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    EntranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketEntrances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketEntrances_Entrances_EntranceId",
                        column: x => x.EntranceId,
                        principalTable: "Entrances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketEntrances_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntrances_EntranceId",
                table: "TicketEntrances",
                column: "EntranceId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntrances_TicketId_EntranceId",
                table: "TicketEntrances",
                columns: new[] { "TicketId", "EntranceId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketEntrances");
        }
    }
}
