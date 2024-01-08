using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrelloBack.Models;

public partial class DbTrelloContext : DbContext
{
    public DbTrelloContext()
    {
    }

    public DbTrelloContext(DbContextOptions<DbTrelloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carte> Cartes { get; set; }

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<Liste> Listes { get; set; }

    public virtual DbSet<Projet> Projets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=db_trello.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carte>(entity =>
        {
            entity.ToTable("Carte");

            entity.Property(e => e.DateCreation).HasColumnType("DATE");

            entity.HasOne(d => d.IdListeNavigation).WithMany(p => p.Cartes).HasForeignKey(d => d.IdListe);
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.ToTable("Commentaire");

            entity.Property(e => e.DateCreation).HasColumnType("DATE");

            entity.HasOne(d => d.IdCarteNavigation).WithMany(p => p.Commentaires).HasForeignKey(d => d.IdCarte);
        });

        modelBuilder.Entity<Liste>(entity =>
        {
            entity.ToTable("Liste");

            entity.HasOne(d => d.IdProjetNavigation).WithMany(p => p.Listes).HasForeignKey(d => d.IdProjet);
        });

        modelBuilder.Entity<Projet>(entity =>
        {
            entity.ToTable("Projet");

            entity.Property(e => e.DateCreation).HasColumnType("DATE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
