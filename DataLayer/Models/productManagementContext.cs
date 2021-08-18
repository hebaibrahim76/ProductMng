using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.Models
{
    public partial class productManagementContext : DbContext
    {
        public productManagementContext()
        {
        }

        public productManagementContext(DbContextOptions<productManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=productManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__category__E548B6737CBA12DD");

                entity.ToTable("category");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdPr)
                    .HasName("PK__products__0148A34ED32BE227");

                entity.ToTable("products");

                entity.Property(e => e.IdPr).HasColumnName("id_pr");

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Expiraydate)
                    .HasColumnType("date")
                    .HasColumnName("expiraydate");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.SubCategoryId).HasColumnName("subCategory_id");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK__products__subCat__3D5E1FD2");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.IdSub)
                    .HasName("PK__subCateg__6D6C23544F3393CE");

                entity.ToTable("subCategory");

                entity.Property(e => e.IdSub).HasColumnName("id_sub");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK__subCatego__id_ca__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
