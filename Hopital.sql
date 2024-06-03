CREATE DATABASE Hopital;

Use Hopital;
    
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
    date DATE,
    medecin NVARCHAR(50),
    num_salle INT,
    tarif DECIMAL(10, 2),
    FOREIGN KEY (idpatient) REFERENCES patients(id)
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


INSERT INTO visites (idpatient, date, medecin, num_salle, tarif) VALUES
(1, '2024-06-05', 'Dr. Maboul', 1, 25.00),
(2, '2024-06-04', 'Dr. Who', 2, 25.00),
(3, '2024-06-03', 'Dr. Maboul', 1, 25.00);

INSERT INTO authentification (login, password, nom, metier) VALUES
('secretaire', 'secretaire123', 'Secretaire', 0),
('medecin1', 'medecin123', 'Dr. Maboul', 1),
('medecin2', 'medecin123', 'Dr. Who', 2);
