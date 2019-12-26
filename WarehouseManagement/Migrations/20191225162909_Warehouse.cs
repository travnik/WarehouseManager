using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseManagement.Migrations
{
    public partial class Warehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "WarehouseProducts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_WarehouseProducts_ProductId_WarehouseId",
                table: "WarehouseProducts",
                columns: new[] { "ProductId", "WarehouseId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_WarehouseProducts_ProductId_WarehouseId",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WarehouseProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts",
                columns: new[] { "ProductId", "WarehouseId" });
        }
    }
}
