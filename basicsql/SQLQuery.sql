CREATE TABLE People (
ID int IDENTITY(1,1) PRIMARY KEY,
Name varchar(255)
);

CREATE TABLE Items (
ID int IDENTITY(1,1) PRIMARY KEY,
PersonID int FOREIGN KEY REFERENCES People(ID),
Name varchar(255)
);

INSERT INTO People VALUES ('Eidan');
INSERT INTO People VALUES ('Doron');
INSERT INTO Items VALUES (1,'Laptop');

SELECT * FROM People;