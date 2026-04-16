CREATE TABLE [dbo].[Patients]
(
	
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    ContactNum VARCHAR(20)

)
