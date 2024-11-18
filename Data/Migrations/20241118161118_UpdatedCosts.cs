using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektZespolowy.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OrderDetails",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Order",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Order",
                newName: "Date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Period",
                table: "Costs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Costs");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderDetails",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Order",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Order",
                newName: "date");
        }
    }
}
