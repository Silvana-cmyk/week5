--create database Studio

--CREATE TABLE Studente(
--	ID INT IDENTITY(1,1),
--	Nome VARCHAR(50) NOT NULL,
--	Cognome VARCHAR(20) NOT NULL,
--	AnnoNascita INT NOT NULL,
--	PRIMARY KEY (ID)
--)

--CREATE TABLE Esame(
--	ID INT IDENTITY(1,1),
--	Nome VARCHAR(50) NOT NULL,
--	CFU INT NOT NULL,
--	DataE DATETIME NOT NULL,
--	Votazione INT,
--	Passato VARCHAR(50) NOT NULL,
--	StudenteID INT NOT NULL,
--	PRIMARY KEY (ID),
--	FOREIGN KEY (StudenteID) REFERENCES Studente(ID),
--)
-------------------------------
--INSERT INTO Studente VALUES('Tizio', 'A', 1999)
--INSERT INTO Studente VALUES('Caio', 'B', 1998)

select *
from studente

select * 
from esame

