IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Hopital')
BEGIN
    DROP DATABASE Hopital;
END;
GO

CREATE DATABASE Hopital;
GO

USE Hopital;
GO

IF OBJECT_ID('dbo.Visites', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Visites;
END;
GO

IF OBJECT_ID('dbo.Patients', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Patients;
END;
GO
    
CREATE TABLE patients (
    id INT PRIMARY KEY IDENTITY(1,1),
    nom NVARCHAR(50),
    prenom NVARCHAR(50),
    age INT,
    adresse NVARCHAR(100),
    telephone NVARCHAR(20)
);

CREATE TABLE visites (
    id INT PRIMARY KEY IDENTITY(1,1),
    idpatient INT,
    date DATETIME,
    medecin NVARCHAR(50),
    num_salle INT,
    tarif DECIMAL(10, 2),
    FOREIGN KEY (idpatient) REFERENCES patients(id),
    dureeAttente FLOAT
);


CREATE TABLE authentification (
    login NVARCHAR(50) PRIMARY KEY,
    password NVARCHAR(50),
    nom NVARCHAR(50),
    metier INT
);

INSERT INTO patients (nom, prenom, age, adresse, telephone) VALUES
('Wayne', 'Bruce', 30, '1 rue Je sais pas', '0612345678'),
('Kent', 'Clark', 40, '18 rue ou ça', '0625840335'),
('Wonka', 'Willy', 25, '23 par ici', '0604128549');


INSERT INTO visites (idpatient, date, medecin, num_salle, tarif, dureeAttente) VALUES
(1, '2024-06-05 15:06', 'Dr. Maboul', 1, 25.00, 96.0),
(2, '2024-06-04 16:00', 'Dr. Who', 2, 25.00, 15.0),
(3, '2024-06-03 18:30', 'Dr. Maboul', 1, 25.00, 162.0);

INSERT INTO authentification (login, password, nom, metier) VALUES
('secretaire', 'secretaire123', 'Secretaire', 0),
('medecin1', 'medecin123', 'Dr. Maboul', 1),
('medecin2', 'medecin123', 'Dr. Who', 2),
('admin', 'admin123', 'Administrateur', -1);

