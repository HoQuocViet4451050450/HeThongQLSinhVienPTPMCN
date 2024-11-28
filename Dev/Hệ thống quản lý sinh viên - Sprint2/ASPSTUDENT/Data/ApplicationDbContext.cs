using ASPSTUDENT.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPSTUDENT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets cho các entity
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<NhomTinNhan> NhomTinNhans { get; set; }
        public DbSet<ThanhVienNhom> ThanhVienNhoms { get; set; }
        public DbSet<TinNhan> TinNhans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa khóa chính cho các entity
            modelBuilder.Entity<SinhVien>()
                .HasKey(s => s.MaSinhVien);  // Khóa chính cho SinhVien

            modelBuilder.Entity<LopHoc>()
                .HasKey(l => l.MaLop);  // Khóa chính cho LopHoc

            modelBuilder.Entity<NguoiDung>()
                .HasKey(nd => nd.MaNguoiDung);  // Khóa chính cho NguoiDung

            modelBuilder.Entity<TinNhan>()
                .HasKey(t => t.MaTinNhan);  // Khóa chính cho TinNhan

            modelBuilder.Entity<NhomTinNhan>()
                .HasKey(n => n.MaNhom);  // Khóa chính cho NhomTinNhan

            // Định nghĩa các quan hệ giữa các entity

            // Quan hệ giữa SinhVien và LopHoc (Nhiều SinhVien thuộc một LopHoc)
            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.LopHoc)
                .WithMany(l => l.SinhViens)
                .HasForeignKey(s => s.MaLop)
                .OnDelete(DeleteBehavior.NoAction);  // Cấm hành động xóa hoặc cập nhật tự động

            // Quan hệ nhiều-nhiều giữa NhomTinNhan và NguoiDung thông qua ThanhVienNhom
            modelBuilder.Entity<ThanhVienNhom>()
                .HasKey(tv => new { tv.MaNhom, tv.MaNguoiDung });  // Khóa chính của bảng liên kết

            modelBuilder.Entity<ThanhVienNhom>()
                .HasOne(tv => tv.NhomTinNhan)
                .WithMany(n => n.ThanhVienNhoms)
                .HasForeignKey(tv => tv.MaNhom)
                .OnDelete(DeleteBehavior.NoAction);  // Cấm hành động xóa hoặc cập nhật tự động

            modelBuilder.Entity<ThanhVienNhom>()
                .HasOne(tv => tv.NguoiDung)
                .WithMany(u => u.NhomThamGia)
                .HasForeignKey(tv => tv.MaNguoiDung)
                .OnDelete(DeleteBehavior.NoAction);  // Cấm hành động xóa hoặc cập nhật tự động

            // Quan hệ giữa TinNhan và NhomTinNhan (Mỗi TinNhan thuộc một NhomTinNhan)
            modelBuilder.Entity<TinNhan>()
                .HasOne(t => t.NhomTinNhan)
                .WithMany(n => n.TinNhans)
                .HasForeignKey(t => t.MaNhom)
                .OnDelete(DeleteBehavior.NoAction);  // Cấm hành động xóa tự động

            // Quan hệ giữa TinNhan và NguoiGui (Mỗi TinNhan được gửi bởi một NguoiGui)
            modelBuilder.Entity<TinNhan>()
                .HasOne(t => t.NguoiGui)
                .WithMany(u => u.TinNhans)
                .HasForeignKey(t => t.NguoiGuiId)
                .OnDelete(DeleteBehavior.NoAction);  // Cấm hành động xóa tự động
        }
    }
}
