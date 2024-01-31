using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoProject.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Meja = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TMDish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMDish", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TTCustDish",
                columns: table => new
                {
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    IdDish = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTCustDish", x => new { x.IdCustomer, x.IdDish });
                    table.ForeignKey(
                        name: "FK_TTCustDish_TMCustomer_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "TMCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TTCustDish_TMDish_IdDish",
                        column: x => x.IdDish,
                        principalTable: "TMDish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TTCustDish_IdDish",
                table: "TTCustDish",
                column: "IdDish");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TTCustDish");

            migrationBuilder.DropTable(
                name: "TMCustomer");

            migrationBuilder.DropTable(
                name: "TMDish");
        }
    }
}
