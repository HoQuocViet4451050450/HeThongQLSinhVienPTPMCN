using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPSTUDENT4.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHoTenColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "ChiTietSinhViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "ChiTietSinhViens");

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    MaSV = table.Column<int>(type: "int", nullable: false),
                    MaLop = table.Column<int>(type: "int", nullable: false),
                    LopHocMaLop = table.Column<int>(type: "int", nullable: false),
                    Diem = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => new { x.MaSV, x.MaLop });
                    table.ForeignKey(
                        name: "FK_Grades_Classes_LopHocMaLop",
                        column: x => x.LopHocMaLop,
                        principalTable: "Classes",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Classes_MaLop",
                        column: x => x.MaLop,
                        principalTable: "Classes",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Students_MaSV",
                        column: x => x.MaSV,
                        principalTable: "Students",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_LopHocMaLop",
                table: "Grades",
                column: "LopHocMaLop");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_MaLop",
                table: "Grades",
                column: "MaLop");
        }
    }
}
