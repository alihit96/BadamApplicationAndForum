using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BadamApplicationAndForum.Data.Migrations
{
    public partial class EditMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReply");

            migrationBuilder.DropTable(
                name: "DirectMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfessorId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectMessage_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectMessage_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectMessage_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageReply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DirectMessageId = table.Column<int>(type: "int", nullable: true),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReply_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageReply_DirectMessage_DirectMessageId",
                        column: x => x.DirectMessageId,
                        principalTable: "DirectMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_ApplicationUserId",
                table: "DirectMessage",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_IdentityUserId",
                table: "DirectMessage",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_ProfessorId",
                table: "DirectMessage",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReply_DirectMessageId",
                table: "MessageReply",
                column: "DirectMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReply_IdentityUserId",
                table: "MessageReply",
                column: "IdentityUserId");
        }
    }
}
