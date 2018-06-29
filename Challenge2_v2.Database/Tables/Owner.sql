﻿CREATE TABLE [dbo].[Owner]
(
	[OwnerID] INT NOT NULL,
	[Surname] VARCHAR(50) NOT NULL,
	[GivenName] VARCHAR(50) NOT NULL,
	[Phone] VARCHAR(30) NOT NULL,
	CONSTRAINT PK_Owner PRIMARY KEY (OwnerID),
	CONSTRAINT CK_Owner_Phone UNIQUE (Phone)
)