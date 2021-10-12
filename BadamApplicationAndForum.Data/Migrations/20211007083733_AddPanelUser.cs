using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BadamApplicationAndForum.Data.Migrations
{
    public partial class AddPanelUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PanelUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                        name: "FK_DirectMessage_AspNetUsers_PanelUserId",
                        column: x => x.PanelUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageReply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DirectMessageId = table.Column<int>(type: "int", nullable: true),
                    PanelUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReply_AspNetUsers_PanelUserId",
                        column: x => x.PanelUserId,
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
                name: "IX_DirectMessage_PanelUserId",
                table: "DirectMessage",
                column: "PanelUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReply_DirectMessageId",
                table: "MessageReply",
                column: "DirectMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReply_PanelUserId",
                table: "MessageReply",
                column: "PanelUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReply");

            migrationBuilder.DropTable(
                name: "DirectMessage");
        }
    }
}
