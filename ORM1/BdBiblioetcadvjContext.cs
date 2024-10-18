using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiBiblioteca.ORM;

public partial class BdBiblioetcadvjContext : DbContext
{
    public BdBiblioetcadvjContext()
    {
    }

    public BdBiblioetcadvjContext(DbContextOptions<BdBiblioetcadvjContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCategoria> TbCategorias { get; set; }

    public virtual DbSet<TbEmprestimo> TbEmprestimos { get; set; }

    public virtual DbSet<TbLivro> TbLivros { get; set; }

    public virtual DbSet<TbMembro> TbMembros { get; set; }

    public virtual DbSet<TbReserva> TbReservas { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_15\\SQLEXPRESS;Database=BD_BIBLIOETCADVJ;User Id=dvj;Password=admin1234;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCategoria>(entity =>
        {
            entity.ToTable("TB_CATEGORIAS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<TbEmprestimo>(entity =>
        {
            entity.ToTable("TB_EMPRESTIMOS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataDevolucao).HasColumnName("dataDevolucao");
            entity.Property(e => e.DataEmprestimo).HasColumnName("dataEmprestimo");

            entity.HasOne(d => d.FkLivroNavigation).WithMany(p => p.TbEmprestimos)
                .HasForeignKey(d => d.FkLivro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_EMPRESTIMOS_TB_LIVROS");

            entity.HasOne(d => d.FkMembroNavigation).WithMany(p => p.TbEmprestimos)
                .HasForeignKey(d => d.FkMembro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_EMPRESTIMOS_TB_MEMBROS");
        });

        modelBuilder.Entity<TbLivro>(entity =>
        {
            entity.ToTable("TB_LIVROS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnoPublicado).HasColumnName("anoPublicado");
            entity.Property(e => e.Autor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autor");
            entity.Property(e => e.Disponibilidade).HasColumnName("disponibilidade");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.FkCategoriaNavigation).WithMany(p => p.TbLivros)
                .HasForeignKey(d => d.FkCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_LIVROS_TB_CATEGORIAS");
        });

        modelBuilder.Entity<TbMembro>(entity =>
        {
            entity.ToTable("TB_MEMBROS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataCadastro).HasColumnName("dataCadastro");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefone");
            entity.Property(e => e.TipoMembro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoMembro");
        });

        modelBuilder.Entity<TbReserva>(entity =>
        {
            entity.ToTable("TB_RESERVAS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataReserva).HasColumnName("dataReserva");

            entity.HasOne(d => d.FkLivroNavigation).WithMany(p => p.TbReservas)
                .HasForeignKey(d => d.FkLivro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_RESERVAS_TB_LIVROS");

            entity.HasOne(d => d.FkMembroNavigation).WithMany(p => p.TbReservas)
                .HasForeignKey(d => d.FkMembro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TB_RESERVAS_TB_MEMBROS");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
