using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Soft.Migrations
{
    /// <inheritdoc />
    public partial class InitCreateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagnosisNameId",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiagnosisNameId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
