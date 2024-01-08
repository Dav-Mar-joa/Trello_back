-- SQLite
-- Création de la table Projet

DROP TABLE IF EXISTS Commentaire;
DROP TABLE IF EXISTS Carte;
DROP TABLE IF EXISTS Liste;
DROP TABLE IF EXISTS Projet;


CREATE TABLE IF NOT EXISTS Projet (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Nom TEXT NOT NULL,
    Description TEXT,
    DateCreation DATE
);

-- Création de la table Liste
CREATE TABLE IF NOT EXISTS Liste (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Nom TEXT NOT NULL,
    IdProjet INTEGER,
    FOREIGN KEY (IdProjet) REFERENCES Projet(Id)
);

-- Création de la table Carte
CREATE TABLE IF NOT EXISTS Carte (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Titre TEXT NOT NULL,
    Description TEXT,
    DateCreation DATE,
    IdListe INTEGER,
    FOREIGN KEY (IdListe) REFERENCES Liste(Id)
);

-- Création de la table Commentaire
CREATE TABLE IF NOT EXISTS Commentaire (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Contenu TEXT,
    DateCreation DATE,
    IdCarte INTEGER,
    FOREIGN KEY (IdCarte) REFERENCES Carte(Id)
);

-- Ajout de données dans la table Projet
INSERT INTO Projet (Nom, Description, DateCreation) VALUES
    ('Projet A', 'Description du Projet A', '2024-01-03'),
    ('Projet B', 'Description du Projet B', '2024-01-04'),
    ('Projet C', 'Description du Projet C', '2024-01-04');

-- Ajout de données dans la table Liste
INSERT INTO Liste (Nom, IdProjet) VALUES
    ('Liste 1', 1),
    ('Liste 2', 2),
    ('Liste 3', 1);

-- Ajout de données dans la table Carte
INSERT INTO Carte (Titre, Description, DateCreation, IdListe) VALUES
    ('Carte 1', 'Description de la Carte 1', '2024-01-05', 1),
    ('Carte 2', 'Description de la Carte 2', '2024-01-06', 2),
    ('Carte 3', 'Description de la Carte 3', '2024-01-07', 1);

-- Ajout de données dans la table Commentaire
INSERT INTO Commentaire (Contenu, DateCreation, IdCarte) VALUES
    ('Commentaire sur Carte 1', '2024-01-08', 1),
    ('Commentaire sur Carte 2', '2024-01-09', 2),
    ('Commentaire sur Carte 3', '2024-01-10', 1);
