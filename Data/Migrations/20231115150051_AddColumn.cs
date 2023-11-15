using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cari.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kalan_Tutar",
                table: "CariHareket");

            migrationBuilder.RenameColumn(
                name: "Islem_Turu",
                table: "CariHareket",
                newName: "IslemTuru");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Il",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ilce",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VergiDairesi",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VergiNo",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Il",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Ilce",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "VergiDairesi",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "VergiNo",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "IslemTuru",
                table: "CariHareket",
                newName: "Islem_Turu");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Kalan_Tutar",
                table: "CariHareket",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
