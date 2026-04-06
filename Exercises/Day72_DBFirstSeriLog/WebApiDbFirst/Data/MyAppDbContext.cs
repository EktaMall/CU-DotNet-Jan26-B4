using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiDbFirst.Models;

namespace WebApiDbFirst.Data;

public partial class MyAppDbContext : DbContext
{
    public MyAppDbContext()
    {
    }

    public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FirstTable> FirstTables { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=TryDatabase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Facultyid).HasName("PK__faculty__DBBE939981E682AF");

            entity.ToTable("faculty");

            entity.Property(e => e.Facultyid)
                .ValueGeneratedNever()
                .HasColumnName("facultyid");
            entity.Property(e => e.Facultyname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("facultyname");
            entity.Property(e => e.Subjectname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("subjectname");
        });

        modelBuilder.Entity<FirstTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FirstTable");

            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.RollNo).HasName("PK__Students__7886D5A0ED689826");

            entity.Property(e => e.RollNo).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
