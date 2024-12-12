﻿// <auto-generated />
using System;
using ASPSTUDENT4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASPSTUDENT4.Migrations
{
    [DbContext(typeof(ASPSTUDENTContext))]
    partial class ASPSTUDENTContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.BaiDang", b =>
                {
                    b.Property<int>("MaBaiDang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBaiDang"));

                    b.Property<string>("DuongDanAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaNguoiDung")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaBaiDang");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("BaiDangs");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.BinhLuan", b =>
                {
                    b.Property<int>("MaBinhLuan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBinhLuan"));

                    b.Property<string>("DuongDanAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaBaiDang")
                        .HasColumnType("int");

                    b.Property<int>("MaNguoiDung")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaBinhLuan");

                    b.HasIndex("MaBaiDang");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("BinhLuans");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.ChiTietSinhVien", b =>
                {
                    b.Property<int>("MaSinhVien")
                        .HasColumnType("int");

                    b.Property<decimal>("DiemGPA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MaLop")
                        .HasColumnType("int");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TongTinChi")
                        .HasColumnType("int");

                    b.HasKey("MaSinhVien");

                    b.HasIndex("MaLop");

                    b.ToTable("ChiTietSinhViens");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.LopHoc", b =>
                {
                    b.Property<int>("MaLop")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLop"));

                    b.Property<string>("TenLop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLop");

                    b.ToTable("LopHocs");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.NguoiDung", b =>
                {
                    b.Property<int>("MaNguoiDung")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNguoiDung"));

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoaiNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNguoiDung");

                    b.ToTable("NguoiDungs");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.BaiDang", b =>
                {
                    b.HasOne("HeThongHoTroHocTap_Version2_.Models.NguoiDung", "NguoiDung")
                        .WithMany("BaiDangs")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.BinhLuan", b =>
                {
                    b.HasOne("HeThongHoTroHocTap_Version2_.Models.BaiDang", "BaiDang")
                        .WithMany("BinhLuans")
                        .HasForeignKey("MaBaiDang")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HeThongHoTroHocTap_Version2_.Models.NguoiDung", "NguoiDung")
                        .WithMany("BinhLuans")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BaiDang");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.ChiTietSinhVien", b =>
                {
                    b.HasOne("HeThongHoTroHocTap_Version2_.Models.LopHoc", "LopHoc")
                        .WithMany("ChiTietSinhViens")
                        .HasForeignKey("MaLop")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HeThongHoTroHocTap_Version2_.Models.NguoiDung", "NguoiDung")
                        .WithOne("ChiTietSinhVien")
                        .HasForeignKey("HeThongHoTroHocTap_Version2_.Models.ChiTietSinhVien", "MaSinhVien")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LopHoc");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.BaiDang", b =>
                {
                    b.Navigation("BinhLuans");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.LopHoc", b =>
                {
                    b.Navigation("ChiTietSinhViens");
                });

            modelBuilder.Entity("HeThongHoTroHocTap_Version2_.Models.NguoiDung", b =>
                {
                    b.Navigation("BaiDangs");

                    b.Navigation("BinhLuans");

                    b.Navigation("ChiTietSinhVien")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
