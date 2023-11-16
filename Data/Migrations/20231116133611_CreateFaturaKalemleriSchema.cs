using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cari.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateFaturaKalemleriSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaturaKalemleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tanim = table.Column<string>(maxLength: 256, nullable: false),
                    Ozelligi = table.Column<string>(maxLength: 256, nullable: true),
                    Birimi = table.Column<int>(type: "int", nullable: false),
                    Kdv = table.Column<decimal>(type: "decimal", nullable: false),
                    Tarih = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturaKalemleri", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaturaKalemleri");
        }
    }
}
