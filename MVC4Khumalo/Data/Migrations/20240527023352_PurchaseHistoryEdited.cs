using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC4Khumalo.Data.Migrations
{
    /// <inheritdoc />
    public partial class PurchaseHistoryEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PurchaseHistories",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PurchaseHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PurchaseHistories");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "PurchaseHistories",
                newName: "UserId");
        }
    }
}
