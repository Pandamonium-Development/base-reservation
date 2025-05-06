using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxAuditoryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Tax",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Tax",
                type: "datetime",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Tax",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Tax",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Tax",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Tax");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Tax");
        }
    }
}
