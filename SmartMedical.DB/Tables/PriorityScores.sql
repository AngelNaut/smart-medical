CREATE TABLE [dbo].[PriorityScores]
(
    ScoreID INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentID INT NOT NULL,
    CalculatedScore INT NOT NULL,
    CalculationDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID)
        ON DELETE CASCADE 
        ON UPDATE CASCADE
)
