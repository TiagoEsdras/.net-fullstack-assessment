using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _03_EmployeeMaintenance.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreatedUserPhotoUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "Users");
        }
    }
}
