using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cari.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateFaturaDetaySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaturaDetay",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        FaturaId = table.Column<int>(type: "int", nullable: false),
                        FaturaKalemleriId = table.Column<int>(type: "int", nullable: false),
                        Ozelligi = table.Column<string>(nullable: true),
                        BirimlerId = table.Column<int>(type: "int", nullable: false),
                        Kdv = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                        Miktar = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                        BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        Tarih = table.Column<DateTime>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_FaturaDetay", x => x.Id);
                    });
            migrationBuilder.CreateIndex(
                name: "FaturaIdIndex",
                table: "FaturaDetay",
                column: "FaturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaturaDetay");
        }
    }
}
