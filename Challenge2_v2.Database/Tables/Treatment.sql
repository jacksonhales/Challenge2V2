CREATE TABLE [dbo].[Treatment]
(
	[TreatmentID] INT NOT NULL,
	[Date] DATETIME NOT NULL,
	[Notes] VARCHAR(100) NOT NULL,
	[Price] MONEY NOT NULL,
	[PetID] INT NOT NULL,
	[ProcedureID] INT NOT NULL,
	CONSTRAINT PK_Treatment PRIMARY KEY (TreatmentID),
	CONSTRAINT FK_Treatment_Pet FOREIGN KEY (PetID) REFERENCES Pet (PetID),
	CONSTRAINT FK_Treatment_Procedure FOREIGN KEY (ProcedureID) REFERENCES [Procedure] (ProcedureID)
)
