using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_answerchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelTaskDones_Answers_AnswerId",
                table: "LabelTaskDones");

            migrationBuilder.DropIndex(
                name: "IX_LabelTaskDones_AnswerId",
                table: "LabelTaskDones");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "LabelTaskDones");

            migrationBuilder.AddColumn<bool>(
                name: "Answer",
                table: "LabelTaskDones",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "LabelTaskDones");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "LabelTaskDones",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LabelTaskDones_AnswerId",
                table: "LabelTaskDones",
                column: "AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelTaskDones_Answers_AnswerId",
                table: "LabelTaskDones",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
