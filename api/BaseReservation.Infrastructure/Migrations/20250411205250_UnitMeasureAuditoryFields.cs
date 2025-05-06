using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UnitMeasureAuditoryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "UnitMeasure",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UnitMeasure",
                type: "datetime",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UnitMeasure",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "UnitMeasure",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "UnitMeasure",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UnitMeasure");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "UnitMeasure");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UnitMeasure");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "UnitMeasure");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UnitMeasure");
        }
    }
}
