CREATE TABLE [dbo].[MedicalConditions]
(
    ConditionID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    ConditionName VARCHAR(255) NOT NULL,
    DiagnosisDate DATE,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
)
