using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFinal.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlAddedToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 27, 23, 13, 0, 319, DateTimeKind.Utc).AddTicks(6099),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 27, 18, 16, 16, 167, DateTimeKind.Utc).AddTicks(8507));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 27, 23, 13, 0, 319, DateTimeKind.Utc).AddTicks(5291),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 27, 18, 16, 16, 167, DateTimeKind.Utc).AddTicks(7595));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 27, 18, 16, 16, 167, DateTimeKind.Utc).AddTicks(8507),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 27, 23, 13, 0, 319, DateTimeKind.Utc).AddTicks(6099));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 27, 18, 16, 16, 167, DateTimeKind.Utc).AddTicks(7595),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 27, 23, 13, 0, 319, DateTimeKind.Utc).AddTicks(5291));
        }
    }
}
