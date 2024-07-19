using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GermanVocabularyAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCardModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOpen",
                table: "Cards",
                newName: "IsFinished");

            migrationBuilder.AddColumn<int>(
                name: "SuccessRate",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuccessRate",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "Cards",
                newName: "IsOpen");
        }
    }
}
