using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Cities_city_id",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "city_id",
                table: "Clients",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Clients",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Clients",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_city_id",
                table: "Clients",
                newName: "IX_Clients_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Cities_CityId",
                table: "Clients",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "City_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Cities_CityId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Clients",
                newName: "city_id");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Clients",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Clients",
                newName: "client_id");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_CityId",
                table: "Clients",
                newName: "IX_Clients_city_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Cities_city_id",
                table: "Clients",
                column: "city_id",
                principalTable: "Cities",
                principalColumn: "City_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
