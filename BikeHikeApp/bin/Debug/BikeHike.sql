﻿CREATE TABLE Customers (
	CID			INT IDENTITY(20001,1) PRIMARY KEY,
	FirstName	NVARCHAR(64) NOT  NULL,
	LastName 	NVARCHAR(64) NOT  NULL,
	Email 		NVARCHAR(64) NOT  NULL
);

CREATE TABLE BikeTypes (
	TID					INT IDENTITY(1,1) PRIMARY KEY,
	TypeDescription		NVARCHAR(128)  NOT  NULL,
	PricePerHour 		MONEY  NOT  NULL
);

CREATE TABLE Bikes (
	BID			INT IDENTITY(1001,1) PRIMARY KEY,
	TID			INT NOT NULL FOREIGN KEY REFERENCES BikeTypes(TID),
	Year 		SMALLINT NOT NULL,
	Rented 		BIT NOT NULL
);

CREATE TABLE Rentals (
	RID			INT IDENTITY(1,1) PRIMARY KEY,
	CID			INT NOT NULL FOREIGN KEY REFERENCES Customers(CID),
	BID			INT NOT NULL FOREIGN KEY REFERENCES Bikes(BID),
	StartTime	DATETIME NOT NULL,
	ExpDuration	FLOAT NOT NULL,
	ActDuration	FLOAT,
	TotalPrice	MONEY
);

Drop Table Customers, BikeTypes, Bikes, Rentals;