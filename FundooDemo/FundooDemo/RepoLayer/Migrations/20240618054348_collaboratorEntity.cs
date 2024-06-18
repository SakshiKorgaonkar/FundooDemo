using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class collaboratorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorEntity_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEntity_Email_NoteId",
                table: "CollaboratorEntity",
                columns: new[] { "Email", "NoteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEntity_NoteId",
                table: "CollaboratorEntity",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorEntity");
        }
    }
}
