CREATE TABLE cities(
    city_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    state NVARCHAR(50) NOT NULL,
);

CREATE TABLE clients(
	client_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    gender CHAR(1) NOT NULL,
    birth_date DATE NOT NULL,
    age INT NOT NULL,
    city_id INT,
    CONSTRAINT FK_Clients_Cities FOREIGN KEY (city_id) REFERENCES cities(city_id)
);