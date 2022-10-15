using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elefanti60.Migrations
{
    public partial class OrderItemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "OrderItems");
        }
    }
}
