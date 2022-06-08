using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudGroup_WebApp.Models
{
    public partial class cloudgroupContext : DbContext
    {
        public cloudgroupContext()
        {
        }

        public cloudgroupContext(DbContextOptions<cloudgroupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Folders> Folders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'dbo.Table_1'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=cloudgroup;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.DId);

                entity.ToTable("documents");

                entity.Property(e => e.DId).HasColumnName("D_Id");

                entity.Property(e => e.ContentType)
                    .HasColumnName("Content_Type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DName)
                    .HasColumnName("D_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__documents__Creat__619B8048");

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.FolderId)
                    .HasConstraintName("FK__documents__Folde__628FA481");
            });

            modelBuilder.Entity<Folders>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("folders");

                entity.Property(e => e.FId).HasColumnName("F_Id");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FName)
                    .HasColumnName("F_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Folders)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__folders__Created__5EBF139D");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UId);

                entity.ToTable("users");

                entity.Property(e => e.UId).HasColumnName("U_Id");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
