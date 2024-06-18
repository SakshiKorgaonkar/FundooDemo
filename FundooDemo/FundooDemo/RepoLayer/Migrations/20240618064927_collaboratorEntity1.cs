using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class collaboratorEntity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEntity_Notes_NoteId",
                table: "CollaboratorEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorEntity",
                table: "CollaboratorEntity");

            migrationBuilder.RenameTable(
                name: "CollaboratorEntity",
                newName: "Collaborators");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorEntity_NoteId",
                table: "Collaborators",
                newName: "IX_Collaborators_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorEntity_Email_NoteId",
                table: "Collaborators",
                newName: "IX_Collaborators_Email_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborators",
                table: "Collaborators",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Notes_NoteId",
                table: "Collaborators",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Notes_NoteId",
                table: "Collaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborators",
                table: "Collaborators");

            migrationBuilder.RenameTable(
                name: "Collaborators",
                newName: "CollaboratorEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Collaborators_NoteId",
                table: "CollaboratorEntity",
                newName: "IX_CollaboratorEntity_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Collaborators_Email_NoteId",
                table: "CollaboratorEntity",
                newName: "IX_CollaboratorEntity_Email_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorEntity",
                table: "CollaboratorEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEntity_Notes_NoteId",
                table: "CollaboratorEntity",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
