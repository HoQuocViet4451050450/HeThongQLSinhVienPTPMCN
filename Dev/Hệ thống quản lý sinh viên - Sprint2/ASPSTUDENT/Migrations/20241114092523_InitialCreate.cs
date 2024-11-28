using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPSTUDENT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LopHocs",
                columns: table => new
                {
                    MaLop = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocs", x => x.MaLop);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoaiNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    MaSinhVien = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLop = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TongTinChi = table.Column<int>(type: "int", nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.MaSinhVien);
                    table.ForeignKey(
                        name: "FK_SinhViens_LopHocs_MaLop",
                        column: x => x.MaLop,
                        principalTable: "LopHocs",
                        principalColumn: "MaLop");
                });

            migrationBuilder.CreateTable(
                name: "NhomTinNhans",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiTaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomTinNhans", x => x.MaNhom);
                    table.ForeignKey(
                        name: "FK_NhomTinNhans_NguoiDungs_NguoiTaoId",
                        column: x => x.NguoiTaoId,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhVienNhoms",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    NgayThamGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhVienNhoms", x => new { x.MaNhom, x.MaNguoiDung });
                    table.ForeignKey(
                        name: "FK_ThanhVienNhoms_NguoiDungs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung");
                    table.ForeignKey(
                        name: "FK_ThanhVienNhoms_NhomTinNhans_MaNhom",
                        column: x => x.MaNhom,
                        principalTable: "NhomTinNhans",
                        principalColumn: "MaNhom");
                });

            migrationBuilder.CreateTable(
                name: "TinNhans",
                columns: table => new
                {
                    MaTinNhan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhom = table.Column<int>(type: "int", nullable: false),
                    NguoiGuiId = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianGui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiTinNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuongDanTapTin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinNhans", x => x.MaTinNhan);
                    table.ForeignKey(
                        name: "FK_TinNhans_NguoiDungs_NguoiGuiId",
                        column: x => x.NguoiGuiId,
                        principalTable: "NguoiDungs",
                        principalColumn: "MaNguoiDung");
                    table.ForeignKey(
                        name: "FK_TinNhans_NhomTinNhans_MaNhom",
                        column: x => x.MaNhom,
                        principalTable: "NhomTinNhans",
                        principalColumn: "MaNhom");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhomTinNhans_NguoiTaoId",
                table: "NhomTinNhans",
                column: "NguoiTaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaLop",
                table: "SinhViens",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhVienNhoms_MaNguoiDung",
                table: "ThanhVienNhoms",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_TinNhans_MaNhom",
                table: "TinNhans",
                column: "MaNhom");

            migrationBuilder.CreateIndex(
                name: "IX_TinNhans_NguoiGuiId",
                table: "TinNhans",
                column: "NguoiGuiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "ThanhVienNhoms");

            migrationBuilder.DropTable(
                name: "TinNhans");

            migrationBuilder.DropTable(
                name: "LopHocs");

            migrationBuilder.DropTable(
                name: "NhomTinNhans");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
