CREATE TABLE [dbo].[Table1]
(
	AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DateTime DATETIME NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Pending',
    UrgencyDescription NVARCHAR(10) NOT NULL,
    RequestedAt DATETIME DEFAULT GETDATE(),

   
)

