using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobNetworkAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_JobPosts_JobPostId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_JobPostId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "JobPostId",
                table: "Files");

            migrationBuilder.CreateTable(
                name: "JobPostImageFileJobPosts",
                columns: table => new
                {
                    JobPostId = table.Column<int>(type: "int", nullable: false),
                    JobPostImageFilesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostImageFileJobPosts", x => new { x.JobPostId, x.JobPostImageFilesId });
                    table.ForeignKey(
                        name: "FK_JobPostImageFileJobPosts_Files_JobPostImageFilesId",
                        column: x => x.JobPostImageFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPostImageFileJobPosts_JobPosts_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostImageFileJobPosts_JobPostImageFilesId",
                table: "JobPostImageFileJobPosts",
                column: "JobPostImageFilesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPostImageFileJobPosts");

            migrationBuilder.AddColumn<int>(
                name: "JobPostId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_JobPostId",
                table: "Files",
                column: "JobPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_JobPosts_JobPostId",
                table: "Files",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
