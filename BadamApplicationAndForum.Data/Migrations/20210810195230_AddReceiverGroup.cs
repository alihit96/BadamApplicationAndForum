using Microsoft.EntityFrameworkCore.Migrations;

namespace BadamApplicationAndForum.Data.Migrations
{
    public partial class AddReceiverGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceivingGroup",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivingGroup",
                table: "News");
        }
    }
}
