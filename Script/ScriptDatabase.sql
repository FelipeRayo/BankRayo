CREATE TABLE Person(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ClientId INT UNIQUE,
	Name VARCHAR(50),
	Gender VARCHAR(15),
	Age INT,
	Address VARCHAR(50),
	NumberPhone VARCHAR(15),
	Password VARCHAR(50),
	State BIT NOT NULL
);

CREATE TABLE Account(
	Number INT PRIMARY KEY,
	Type VARCHAR(12) NOT NULL,
	InitialBalance MONEY,
	State BIT NOT NULL,
	IdClient INT

	FOREIGN KEY (IdClient) REFERENCES Person (ClientId)
);

CREATE TABLE Transactions(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	NumberAccount INT,
	Date DATETIME NOT NULL,
	Type VARCHAR(12),
	Value MONEY,
	Balance MONEY,
	State BIT NOT NULL,

	FOREIGN KEY (NumberAccount) REFERENCES Account (Number)
);

