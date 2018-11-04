using Microsoft.EntityFrameworkCore.Migrations;

namespace Chushka.Migrations
{
    public partial class ChannelTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTag_Channels_ChannelId",
                table: "ChannelTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTag_Tags_TagId",
                table: "ChannelTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelTag",
                table: "ChannelTag");

            migrationBuilder.RenameTable(
                name: "ChannelTag",
                newName: "ChannelTags");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelTag_TagId",
                table: "ChannelTags",
                newName: "IX_ChannelTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelTag_ChannelId",
                table: "ChannelTags",
                newName: "IX_ChannelTags_ChannelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelTags",
                table: "ChannelTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTags_Channels_ChannelId",
                table: "ChannelTags",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTags_Tags_TagId",
                table: "ChannelTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTags_Channels_ChannelId",
                table: "ChannelTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ChannelTags_Tags_TagId",
                table: "ChannelTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelTags",
                table: "ChannelTags");

            migrationBuilder.RenameTable(
                name: "ChannelTags",
                newName: "ChannelTag");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelTags_TagId",
                table: "ChannelTag",
                newName: "IX_ChannelTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelTags_ChannelId",
                table: "ChannelTag",
                newName: "IX_ChannelTag_ChannelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelTag",
                table: "ChannelTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTag_Channels_ChannelId",
                table: "ChannelTag",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelTag_Tags_TagId",
                table: "ChannelTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
