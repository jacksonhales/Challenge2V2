/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF '$(LoadTestData)' = 'true'

BEGIN

DELETE FROM [Treatment];
DELETE FROM [Pet];
DELETE FROM [Procedure];
DELETE FROM [Owner];

INSERT INTO [Owner] ([OwnerID], [Surname], [GivenName], [Phone]) VALUES
(1, 'Sinatra', 'Frank', '0400111222'),
(2, 'Ellington', 'Duke', '0400222333'),
(3, 'Fitzgerald', 'Ella', '0400333444');

INSERT INTO [Procedure] ([ProcedureID], [Description]) VALUES
(01, 'Rabies Vaccination'),
(10, 'Examine and Treat Wound'),
(05, 'Heart Worm Test'),
(08, 'Tetnus Vaccination');

INSERT INTO [Pet] ([PetID], [Name], [Type], [OwnerID]) VALUES
(1, 'Buster', 'Dog', 1),
(2, 'Fluffy', 'Cat', 1),
(3, 'Stew', 'Rabbit', 2),
(4, 'Buster', 'Dog', 3),
(5, 'Pooper', 'Dog', 3);

INSERT INTO [Treatment] ([TreatmentID], [Date], [Notes], [Price], [PetID], [ProcedureID]) VALUES
(1, '20/jun/2017', 'Routine Vaccination', $22, 1, 01),
(2, '15/may/2018', 'Booster Shot', $24, 1, 01),
(3, '10/may/2018', 'Wounds sustained in apparent cat fight', $30, 2, 10),
(4, '10/may/2018', 'Wounds sustained during attempt to cook the stew', $30, 3, 10),
(5, '18/may/2018', 'Routine Test', $25, 5, 05);

END;