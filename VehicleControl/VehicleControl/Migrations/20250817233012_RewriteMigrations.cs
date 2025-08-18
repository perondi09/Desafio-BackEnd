using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleControl.Migrations
{
    /// <inheritdoc />
    public partial class RewriteMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Drivers_DriverId1",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Vehicles_VehicleId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DriverId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_VehicleId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "DriverId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "VehicleId1",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Rentals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Rentals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Rentals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DriverId",
                table: "Rentals",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_VehicleId",
                table: "Rentals",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Drivers_DriverId",
                table: "Rentals",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Vehicles_VehicleId",
                table: "Rentals",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Drivers_DriverId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Vehicles_VehicleId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DriverId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_VehicleId",
                table: "Rentals");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleId",
                table: "Rentals",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Rentals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "Rentals",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DriverId1",
                table: "Rentals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId1",
                table: "Rentals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DriverId1",
                table: "Rentals",
                column: "DriverId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_VehicleId1",
                table: "Rentals",
                column: "VehicleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Drivers_DriverId1",
                table: "Rentals",
                column: "DriverId1",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Vehicles_VehicleId1",
                table: "Rentals",
                column: "VehicleId1",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
