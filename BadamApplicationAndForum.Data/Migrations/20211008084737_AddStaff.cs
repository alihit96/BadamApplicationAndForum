using Microsoft.EntityFrameworkCore.Migrations;

namespace BadamApplicationAndForum.Data.Migrations
{
    public partial class AddStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessorInterests");

            migrationBuilder.RenameColumn(
                name: "NationalCode",
                table: "Professors",
                newName: "ProfessorInterests");

            migrationBuilder.AddColumn<string>(
                name: "NormalName",
                table: "Professors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalName",
                table: "Professors");

            migrationBuilder.RenameColumn(
                name: "ProfessorInterests",
                table: "Professors",
                newName: "NationalCode");

            migrationBuilder.CreateTable(
                name: "ProfessorInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessorInterests_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorInterests_ProfessorId",
                table: "ProfessorInterests",
                column: "ProfessorId");
        }
    }
}
