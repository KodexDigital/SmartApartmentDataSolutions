﻿//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace DataAccessLayer.DatabaseContext
//{
//    public partial class SmartApp_DBContext : DbContext
//    {
//        public SmartApp_DBContext()
//        {
//        }

//        public SmartApp_DBContext(DbContextOptions<SmartApp_DBContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
//        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
//        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
//        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
//        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
//        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database = SmartApp_DB;Integrated Security=True");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

//            modelBuilder.Entity<AspNetRole>(entity =>
//            {
//                entity.HasIndex(e => e.Name, "RoleNameIndex")
//                    .IsUnique();

//                entity.Property(e => e.Id).HasMaxLength(128);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(256);
//            });

//            modelBuilder.Entity<AspNetUser>(entity =>
//            {
//                entity.HasIndex(e => e.UserName, "UserNameIndex")
//                    .IsUnique();

//                entity.Property(e => e.Id).HasMaxLength(128);

//                entity.Property(e => e.Email).HasMaxLength(256);

//                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

//                entity.Property(e => e.UserName)
//                    .IsRequired()
//                    .HasMaxLength(256);
//            });

//            modelBuilder.Entity<AspNetUserClaim>(entity =>
//            {
//                entity.HasIndex(e => e.UserId, "IX_UserId");

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(128);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserClaims)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
//            });

//            modelBuilder.Entity<AspNetUserLogin>(entity =>
//            {
//                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
//                    .HasName("PK_dbo.AspNetUserLogins");

//                entity.HasIndex(e => e.UserId, "IX_UserId");

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.ProviderKey).HasMaxLength(128);

//                entity.Property(e => e.UserId).HasMaxLength(128);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserLogins)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
//            });

//            modelBuilder.Entity<AspNetUserRole>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.RoleId })
//                    .HasName("PK_dbo.AspNetUserRoles");

//                entity.HasIndex(e => e.RoleId, "IX_RoleId");

//                entity.HasIndex(e => e.UserId, "IX_UserId");

//                entity.Property(e => e.UserId).HasMaxLength(128);

//                entity.Property(e => e.RoleId).HasMaxLength(128);

//                entity.HasOne(d => d.Role)
//                    .WithMany(p => p.AspNetUserRoles)
//                    .HasForeignKey(d => d.RoleId)
//                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserRoles)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
//            });

//            modelBuilder.Entity<MigrationHistory>(entity =>
//            {
//                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
//                    .HasName("PK_dbo.__MigrationHistory");

//                entity.ToTable("__MigrationHistory");

//                entity.Property(e => e.MigrationId).HasMaxLength(150);

//                entity.Property(e => e.ContextKey).HasMaxLength(300);

//                entity.Property(e => e.Model).IsRequired();

//                entity.Property(e => e.ProductVersion)
//                    .IsRequired()
//                    .HasMaxLength(32);
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
