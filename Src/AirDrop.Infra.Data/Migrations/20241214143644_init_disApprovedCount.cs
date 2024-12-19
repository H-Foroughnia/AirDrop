using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirDrop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_disApprovedCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsApprovedCount",
                table: "TaskImages",
                newName: "DisApprovedCount");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedCount",
                table: "TaskImages",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedCount",
                table: "TaskImages");

            migrationBuilder.RenameColumn(
                name: "DisApprovedCount",
                table: "TaskImages",
                newName: "IsApprovedCount");
        }
    }
}
