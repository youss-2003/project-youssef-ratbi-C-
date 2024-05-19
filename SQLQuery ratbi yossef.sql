create database gde
use gde
create table utilisateur(
email varchar(255),
pass varchar(255)
);
insert into utilisateur values('admin','adminadmin');
select * from utilisateur

CREATE TABLE Ville (
    ID_ville INT PRIMARY KEY,
    Nom_ville VARCHAR(255)
);

CREATE TABLE Quartier (
    ID_quartier INT PRIMARY KEY,
    Nom_quartier VARCHAR(255),
    ID_ville INT,
    FOREIGN KEY (ID_ville) REFERENCES Ville(ID_ville)
);

CREATE TABLE Association (
    ID_Association INT PRIMARY KEY,
    Nom_association VARCHAR(255),
    Description_association TEXT,
    Date_association DATE,
    ID_quartier INT,
    FOREIGN KEY (ID_quartier) REFERENCES Quartier(ID_quartier)
);

CREATE TABLE Annonce (
    ID_Annonce INT PRIMARY KEY,
    Titre_annonce VARCHAR(255),
    Contenu_annonce TEXT,
    Date_publication DATE,
    ID_Association INT,
    FOREIGN KEY (ID_Association) REFERENCES Association(ID_Association)
);

CREATE TABLE Membre (
    ID_Membre INT PRIMARY KEY,
    Nom VARCHAR(100),
    Prénom VARCHAR(100),
    E_mail VARCHAR(255),
    Date_adhésion DATE,
    ID_Association INT,
    FOREIGN KEY (ID_Association) REFERENCES Association(ID_Association)
);

CREATE TABLE Rôles (
    ID_Rôle INT PRIMARY KEY,
    Nom_rôle VARCHAR(100),
    ID_Association INT,
    FOREIGN KEY (ID_Association) REFERENCES Association(ID_Association)
);

CREATE TABLE Événement (
    ID_Événement INT PRIMARY KEY,
    Nom_événement VARCHAR(255),
    Date_événement DATE,
    Lieu_événement VARCHAR(255),
    ID_Association INT,
    FOREIGN KEY (ID_Association) REFERENCES Association(ID_Association)
);

CREATE TABLE Finance (
    ID_Finances INT PRIMARY KEY,
    Date_transaction DATE,
    Montant_transaction DECIMAL(10, 2),
    ID_Association INT,
    FOREIGN KEY (ID_Association) REFERENCES Association(ID_Association)
);

CREATE TABLE Participation (
    ID_Participation INT PRIMARY KEY,
    Date_participation DATE,
    ID_Membre INT,
    ID_Événement INT,
    FOREIGN KEY (ID_Membre) REFERENCES Membre(ID_Membre),
    FOREIGN KEY (ID_Événement) REFERENCES Événement(ID_Événement)
);

CREATE TABLE Comité (
    ID_Comité INT PRIMARY KEY,
    Nom_comité VARCHAR(255),
    Description_comité TEXT,
    ID_Association INT,
    FOREIGN KEY (ID_Association) REFERENCES Association(ID_Association)
);

CREATE TABLE Documents (
    ID_Document INT PRIMARY KEY,
    Nom_document VARCHAR(255),
    Type_document VARCHAR(10),
    ID_Association INT,
    FOREIGN KEY (ID_Association) REFERENCES Association(ID_Association)
);

CREATE TABLE Évaluations (
    ID_Évaluation INT PRIMARY KEY,
    Note INT,
    Commentaire TEXT,
    ID_Membre INT,
    FOREIGN KEY (ID_Membre) REFERENCES Membre(ID_Membre)
);
