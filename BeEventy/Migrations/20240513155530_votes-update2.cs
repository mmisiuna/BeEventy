using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeEventy.Migrations
{
    /// <inheritdoc />
    public partial class votesupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlus",
                table: "Votes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlus",
                table: "Votes");
        }
    }
}
