using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Soft.Migrations
{
    /// <inheritdoc />
    public partial class NewInitUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiagnosisName",
                table: "Diagnoses",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiagnosisNameId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagnosisNameId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "DiagnosisName",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
