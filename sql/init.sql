-- CREATING A DATABASE
CREATE DATABASE SystemApiDB;
GO

USE SystemApiDB;
GO

-- CREATING TABLES
CREATE TABLE cities( city_id INT PRIMARY KEY IDENTITY(1,1), name NVARCHAR(100) NOT NULL, state NVARCHAR(50) NOT NULL);
GO

CREATE TABLE clients( client_id INT PRIMARY KEY IDENTITY(1,1), name NVARCHAR(100) NOT NULL, gender CHAR(1) NOT NULL, birth_date DATE NOT NULL, age INT NOT NULL, city_id INT, CONSTRAINT FK_Clients_Cities FOREIGN KEY (city_id) REFERENCES cities(city_id));
GO

-- INSERTING DATA
INSERT INTO cities (name, state) VALUES ('Petrolina','PE'),('Juazeiro', 'BA'),('Recife','PE'),('Salvador','BA'),('Fortaleza','CE'),('São Paulo','SP'),('Rio de Janeiro','RJ'),('Belo Horizonte','MG'),('Brasília','DF'),('Curitiba','PR');
GO

INSERT INTO clients (name, gender, birth_date, age, city_id) VALUES ('João','M','1998-05-21', 27, 1),('Ana','F','1995-07-12', 30, 2),('José','M','1990-10-15', 35, 3),('Carlos','M','1993-02-18', 32, 4),('Mariana','F','1997-09-25', 28, 5),('Paulo','M','1999-11-30', 26, 1),('Maria','F','1994-04-03', 31, 1),('Pedro','M','1991-08-07', 34, 3),('Lucas','M','1996-01-10', 29, 5),('Fernanda','F','1992-06-14', 33, 6);
GO

-- CREATE A NEW USER TO ACCESS THE DATABASE AND GRANT PERMISSIONS
CREATE LOGIN userSystemAPI WITH PASSWORD = '<YOUR_PASSWORD>';
CREATE USER userSystemAPI FOR LOGIN userSystemAPI;
GRANT SELECT, INSERT, UPDATE, DELETE, CREATE TABLE TO userSystemAPI;
GO

ALTER LOGIN sa DISABLE;
GO

ALTER LOGIN sa WITH NAME = sa_DESATIVADO;
GO