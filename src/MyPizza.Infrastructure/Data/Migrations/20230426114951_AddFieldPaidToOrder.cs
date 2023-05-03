﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPizza.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldPaidToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Orders");
        }
    }
}
