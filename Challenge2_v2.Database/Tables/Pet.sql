CREATE TABLE [dbo].[Pet]
(
	[PetID] INT NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[Type] VARCHAR(20) NOT NULL,
	[OwnerID] INT NOT NULL,
	CONSTRAINT PK_Pet PRIMARY KEY (PetID),
	CONSTRAINT FK_Pet_Owner FOREIGN KEY (OwnerID) REFERENCES Owner (OwnerID),
	CONSTRAINT CK_Pet_Type CHECK ([Type] in ('Bird', 'Dog', 'Cat', 'Fish', 'Rabbit'))
)
