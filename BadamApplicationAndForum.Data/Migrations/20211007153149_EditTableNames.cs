using Microsoft.EntityFrameworkCore.Migrations;

namespace BadamApplicationAndForum.Data.Migrations
{
    public partial class EditTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessage_ApplicationUsers_ApplicationUserId",
                table: "DirectMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessage_AspNetUsers_PanelUserId",
                table: "DirectMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReply_AspNetUsers_PanelUserId",
                table: "MessageReply");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReply_DirectMessage_DirectMessageId",
                table: "MessageReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageReply",
                table: "MessageReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectMessage",
                table: "DirectMessage");

            migrationBuilder.RenameTable(
                name: "MessageReply",
                newName: "MessageReplies");

            migrationBuilder.RenameTable(
                name: "DirectMessage",
                newName: "DirectMessages");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReply_PanelUserId",
                table: "MessageReplies",
                newName: "IX_MessageReplies_PanelUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReply_DirectMessageId",
                table: "MessageReplies",
                newName: "IX_MessageReplies_DirectMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessage_PanelUserId",
                table: "DirectMessages",
                newName: "IX_DirectMessages_PanelUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessage_ApplicationUserId",
                table: "DirectMessages",
                newName: "IX_DirectMessages_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageReplies",
                table: "MessageReplies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectMessages",
                table: "DirectMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_ApplicationUsers_ApplicationUserId",
                table: "DirectMessages",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_AspNetUsers_PanelUserId",
                table: "DirectMessages",
                column: "PanelUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReplies_AspNetUsers_PanelUserId",
                table: "MessageReplies",
                column: "PanelUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReplies_DirectMessages_DirectMessageId",
                table: "MessageReplies",
                column: "DirectMessageId",
                principalTable: "DirectMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_ApplicationUsers_ApplicationUserId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_AspNetUsers_PanelUserId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReplies_AspNetUsers_PanelUserId",
                table: "MessageReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReplies_DirectMessages_DirectMessageId",
                table: "MessageReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageReplies",
                table: "MessageReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirectMessages",
                table: "DirectMessages");

            migrationBuilder.RenameTable(
                name: "MessageReplies",
                newName: "MessageReply");

            migrationBuilder.RenameTable(
                name: "DirectMessages",
                newName: "DirectMessage");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReplies_PanelUserId",
                table: "MessageReply",
                newName: "IX_MessageReply_PanelUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageReplies_DirectMessageId",
                table: "MessageReply",
                newName: "IX_MessageReply_DirectMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessages_PanelUserId",
                table: "DirectMessage",
                newName: "IX_DirectMessage_PanelUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectMessages_ApplicationUserId",
                table: "DirectMessage",
                newName: "IX_DirectMessage_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageReply",
                table: "MessageReply",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirectMessage",
                table: "DirectMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessage_ApplicationUsers_ApplicationUserId",
                table: "DirectMessage",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessage_AspNetUsers_PanelUserId",
                table: "DirectMessage",
                column: "PanelUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReply_AspNetUsers_PanelUserId",
                table: "MessageReply",
                column: "PanelUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReply_DirectMessage_DirectMessageId",
                table: "MessageReply",
                column: "DirectMessageId",
                principalTable: "DirectMessage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
