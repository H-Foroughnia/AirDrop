using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirDrop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_secondDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedCount",
                table: "TaskImages");

            migrationBuilder.DropColumn(
                name: "DisApprovedCount",
                table: "TaskImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedCount",
                table: "TaskImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisApprovedCount",
                table: "TaskImages",
                type: "int",
                nullable: true);
        }
    }
}
