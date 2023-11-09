using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cari.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateCariHareketSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CariHareket",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Firma_Id = table.Column<int>(type: "int", nullable: false),
                        Islem_Turu = table.Column<int>(type: "int", nullable: false),
                        Tutar = table.Column<decimal>(type: "decimal", nullable: false),
                        Kalan_Tutar = table.Column<decimal>(type: "decimal", nullable: true),
                        Tarih = table.Column<DateTime>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_CariHareket", x => x.Id);
                    }
            );

            migrationBuilder.CreateIndex(
                name: "FirmaIdIndex",
                table: "CariHareket",
                column: "Firma_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CariHareket"
            );
        }
    }
}
