﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrelloBack.Models;

public partial class DbTrelloContext : DbContext
{

    bool SQL_Server=true;
    
    public DbTrelloContext()
    {
    }

    public DbTrelloContext(DbContextOptions<DbTrelloContext> options)
        : base(options)
    {
        if (SQL_Server)
        { 
        // Create the database if it doesn't exist
        Database.EnsureCreated();
        // Create the tables if they don't exist
        Database.Migrate();
        // Seed the database if empty
        /*
        if (!Messages.Any())
        {
            Messages.Add(new Message { Text = "Hello" });
            Messages.Add(new Message { Text = "World" });
            SaveChanges();
        }*/

        /*
        Projets.Add(new Projet()
        {
            DateCreation = new DateOnly(),
            Description = "Description",
            Listes = [],
            Nom = "Premier projet"
        });
        
        Listes.Add(new Liste() 
        { 
            IdProjet=1,
            Nom="première liste",
        });
        */

        SaveChanges();
        }
    }

    public virtual DbSet<Carte> Cartes { get; set; }

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<Liste> Listes { get; set; }

    public virtual DbSet<Projet> Projets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
      => optionsBuilder.UseSqlServer("Server=tcp:trellofinalserveur.database.windows.net,1433;Initial Catalog=TrelloDavid;Persist Security Info=False;User ID=MartinsDavid;Password=Dm.131713;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
 //       => optionsBuilder.UseSqlite("Data Source=db_trello.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carte>(entity =>
        {
            entity.ToTable("Carte");

            entity.Property(e => e.dateCreation).HasColumnType("DATE");

            entity.HasOne(d => d.idListeNavigation).WithMany(p => p.cartes).HasForeignKey(d => d.idListe);
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.ToTable("Commentaire");

            entity.Property(e => e.dateCreation).HasColumnType("DATE");

            entity.HasOne(d => d.idCarteNavigation).WithMany(p => p.commentaires).HasForeignKey(d => d.idCarte);
        });

        modelBuilder.Entity<Liste>(entity =>
        {
            entity.ToTable("Liste");

            entity.HasOne(d => d.idProjetNavigation).WithMany(p => p.listes).HasForeignKey(d => d.idProjet);
        });

        modelBuilder.Entity<Projet>(entity =>
        {
            entity.ToTable("Projet");

            entity.Property(e => e.dateCreation).HasColumnType("DATE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
