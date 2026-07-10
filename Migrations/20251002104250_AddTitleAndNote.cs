using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfitCalcApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleAndNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ProfitRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProfitRecords",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "ProfitRecords");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProfitRecords");
        }
    }
}
