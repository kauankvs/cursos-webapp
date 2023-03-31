using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CursosWebApp.Models;

public partial class CursosWebAppContext : DbContext
{
    public CursosWebAppContext()
    {
    }

    public CursosWebAppContext(DbContextOptions<CursosWebAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CursosWebApp;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curso__3214EC07AE61BF84");

            entity.ToTable("Curso");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CapaUrl)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("CapaURL");
            entity.Property(e => e.DataDeCriacao).HasColumnType("date");
            entity.Property(e => e.Descricao)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Criador).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.CriadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curso__CriadorId__4BAC3F29");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07D0234AC2");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Username, "UQ__Usuario__536C85E4C49833F6").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534621A4076").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Papel)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Sobrenome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
