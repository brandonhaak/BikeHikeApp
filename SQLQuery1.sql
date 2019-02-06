UPDATE Bikes
SET Rented = 1
where BID = '1001';

SELECT *
FROM Customers;

Select CID from Customers
where CID = '20001';

INSERT INTO 
Rentals(CID, NumBikes, StartTime, ExpDuration)
VALUES ('20001', '4', '01:01:01:123AM', '2.0');

Select *
FROM Rentals;

Delete Rentals;

drop table Rentals;

CREATE TABLE Rentals (
	RID			INT IDENTITY(1,1) PRIMARY KEY,
	CID			INT NOT NULL FOREIGN KEY REFERENCES Customers(CID),
	StartTime	DATETIME NOT NULL,
	ExpDuration	FLOAT NOT NULL,
	NumBikes 	SMALLINT NOT NULL,
	ActDuration	FLOAT,
	TotalPrice	MONEY
);