using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPSTUDENT4.Migrations
{
    /// <inheritdoc />
    public partial class AddGradeStudentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LopHocMaLop",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LopHocMaLop",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_LopHocMaLop",
                table: "Students",
                column: "LopHocMaLop");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_LopHocMaLop",
                table: "Grades",
                column: "LopHocMaLop");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Classes_LopHocMaLop",
                table: "Grades",
                column: "LopHocMaLop",
                principalTable: "Classes",
                principalColumn: "MaLop",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_LopHocMaLop",
                table: "Students",
                column: "LopHocMaLop",
                principalTable: "Classes",
                principalColumn: "MaLop",
               onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Classes_LopHocMaLop",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_LopHocMaLop",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_LopHocMaLop",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Grades_LopHocMaLop",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "LopHocMaLop",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LopHocMaLop",
                table: "Grades");
        }
    }
}
