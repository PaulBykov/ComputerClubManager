using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerClub.Migrations
{
    /// <inheritdoc />
    public partial class Relations_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clubs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__clubs__3213E83F9C5F08A6", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rates",
                columns: table => new
                {
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__rates__72E12F1A9788C4A4", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "rents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    length = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__rents__3213E83FB4F8AF02", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "incomes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    club_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__incomes__3213E83F4C68E736", x => x.id);
                    table.ForeignKey(
                        name: "FK__incomes__clubId__3C69FB99",
                        column: x => x.club_id,
                        principalTable: "clubs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "stuff",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    club_id = table.Column<int>(type: "int", nullable: false),
                    pass_hash = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__stuff__clubId__3E52440B",
                        column: x => x.club_id,
                        principalTable: "clubs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "computers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    club_id = table.Column<int>(type: "int", nullable: false),
                    rate_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    rentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__computer__3213E83F569F54ED", x => x.id);
                    table.ForeignKey(
                        name: "FK__computers__clubI__5812160E",
                        column: x => x.club_id,
                        principalTable: "clubs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__computers__rateN__59063A47",
                        column: x => x.rate_name,
                        principalTable: "rates",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "FK__computers__rentI__59FA5E80",
                        column: x => x.rentId,
                        principalTable: "rents",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_computers_club_id",
                table: "computers",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "IX_computers_rate_name",
                table: "computers",
                column: "rate_name");

            migrationBuilder.CreateIndex(
                name: "IX_computers_rentId",
                table: "computers",
                column: "rentId");

            migrationBuilder.CreateIndex(
                name: "IX_incomes_club_id",
                table: "incomes",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "IX_stuff_club_id",
                table: "stuff",
                column: "club_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "computers");

            migrationBuilder.DropTable(
                name: "incomes");

            migrationBuilder.DropTable(
                name: "stuff");

            migrationBuilder.DropTable(
                name: "rates");

            migrationBuilder.DropTable(
                name: "rents");

            migrationBuilder.DropTable(
                name: "clubs");
        }
    }
}
