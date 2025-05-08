using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Soft.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicineName",
                table: "Medicines",
                newName: "Medicine");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Diagnosis",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiagnosisId",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "MedicalRecords");

            migrationBuilder.RenameColumn(
                name: "Medicine",
                table: "Medicines",
                newName: "MedicineName");

            migrationBuilder.AlterColumn<int>(
                name: "Diagnosis",
                table: "MedicalRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
