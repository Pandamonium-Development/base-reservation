using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TokenMasterUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "TokenMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TokenMaster",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TokenMaster",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "TokenMaster",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TokenMaster",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "TokenMaster");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TokenMaster");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TokenMaster");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "TokenMaster");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TokenMaster");
        }
    }
}
