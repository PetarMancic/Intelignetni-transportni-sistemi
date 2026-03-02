using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeltaDrive.Migrations
{
    /// <inheritdoc />
    public partial class AddingPassengerIdToRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassengerId",
                table: "Ratings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PassengerId",
                table: "Ratings",
                column: "PassengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Passengers_PassengerId",
                table: "Ratings",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Passengers_PassengerId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_PassengerId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "Ratings");
        }
    }
}
