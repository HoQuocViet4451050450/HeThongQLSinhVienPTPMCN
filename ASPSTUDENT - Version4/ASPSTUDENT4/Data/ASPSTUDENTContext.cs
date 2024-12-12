using ASPSTUDENT4.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPSTUDENT4.Data
{
    public class ASPSTUDENTContext : DbContext
    {
        public ASPSTUDENTContext(DbContextOptions<ASPSTUDENTContext> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<BaiDang> BaiDangs { get; set; }
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<ChiTietSinhVien> ChiTietSinhViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationships and constraints

            // NguoiDung -> BaiDang (One to Many)
            modelBuilder.Entity<BaiDang>()
                .HasKey(b => b.MaBaiDang);

            modelBuilder.Entity<BaiDang>()
                .HasOne(b => b.NguoiDung)
                .WithMany(n => n.BaiDangs)
                .HasForeignKey(b => b.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);

            // NguoiDung -> BinhLuan (One to Many)
            modelBuilder.Entity<BinhLuan>()
                .HasOne(b => b.NguoiDung)
                .WithMany(n => n.BinhLuans)
                .HasForeignKey(b => b.MaNguoiDung)
                .OnDelete(DeleteBehavior.Restrict);

            // BaiDang -> BinhLuan (One to Many)
            modelBuilder.Entity<BinhLuan>()
                .HasOne(b => b.BaiDang)
                .WithMany(bd => bd.BinhLuans)
                .HasForeignKey(b => b.MaBaiDang)
                .OnDelete(DeleteBehavior.Restrict);

            // NguoiDung -> ChiTietSinhVien (One to One)
            modelBuilder.Entity<ChiTietSinhVien>()
                .HasKey(c => c.MaSinhVien);

            modelBuilder.Entity<ChiTietSinhVien>()
                .HasOne(c => c.NguoiDung)
                .WithOne(n => n.ChiTietSinhVien)
                .HasForeignKey<ChiTietSinhVien>(c => c.MaSinhVien)
                .OnDelete(DeleteBehavior.Restrict);

            // ChiTietSinhVien -> LopHoc (Many to One)
            modelBuilder.Entity<ChiTietSinhVien>()
                .HasOne(c => c.LopHoc)
                .WithMany(l => l.ChiTietSinhViens)
                .HasForeignKey(c => c.MaLop)
                .OnDelete(DeleteBehavior.Restrict);

            // Set default value for NgayTao columns
            modelBuilder.Entity<NguoiDung>()
                .Property(n => n.NgayTao)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<BaiDang>()
                .Property(b => b.NgayTao)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<BinhLuan>()
                .Property(b => b.NgayTao)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
