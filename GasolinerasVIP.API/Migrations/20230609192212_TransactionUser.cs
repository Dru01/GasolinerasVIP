using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasolinerasVIP.API.Migrations
{
    /// <inheritdoc />
    public partial class TransactionUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Transaction");
        }
    }
}
