using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priotity",
                table: "tblProductImages",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tblProductImages",
                newName: "FileName");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "tblProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "tblProducts");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "tblProductImages",
                newName: "Priotity");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "tblProductImages",
                newName: "Name");
        }
    }
}
