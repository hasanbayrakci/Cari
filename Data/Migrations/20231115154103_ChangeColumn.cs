using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cari.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ilce",
                table: "Customer",
                newName: "IlceId");

            migrationBuilder.RenameColumn(
                name: "Il",
                table: "Customer",
                newName: "IlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IlceId",
                table: "Customer",
                newName: "Ilce");

            migrationBuilder.RenameColumn(
                name: "IlId",
                table: "Customer",
                newName: "Il");
        }
    }
}
