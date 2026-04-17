using SmartMedical.Application.Services;
using SmartMedical.Domain.Entities;

namespace SmartMedical.Test;

public class PriorityTests
{
    [Fact]
    public void CalculateScore_EmergencyUrgency_ReturnsHighScore()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Juan",
            LastName = "Perez",
            DateOfBirth = new DateTime(1990, 5, 15),
            ContactNum = "809-555-1234"
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Emergencia",
            Status = "Pending",
            RequestedAt = DateTime.Now.AddDays(-1)
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score >= 50, $"Score should be >= 50 for emergency, got {score}");
    }

    [Fact]
    public void CalculateScore_UrgentUrgency_ReturnsMediumHighScore()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Juan",
            LastName = "Perez",
            DateOfBirth = new DateTime(1990, 5, 15),
            ContactNum = "809-555-1234"
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Urgente",
            Status = "Pending",
            RequestedAt = DateTime.Now.AddDays(-1)
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score >= 30, $"Score should be >= 30 for urgent, got {score}");
    }

    [Fact]
    public void CalculateScore_RoutineCheckup_ReturnsLowScore()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Juan",
            LastName = "Perez",
            DateOfBirth = new DateTime(1990, 5, 15),
            ContactNum = "809-555-1234"
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(10),
            UrgencyDescription = "Chequeo de rutina",
            Status = "Pending",
            RequestedAt = DateTime.Now
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score < 60, $"Score should be < 60 for routine, got {score}");
    }

    [Fact]
    public void CalculateScore_SeniorPatient_Age65Plus_AddsBonusPoints()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Maria",
            LastName = "Garcia",
            DateOfBirth = new DateTime(1955, 1, 1),
            ContactNum = "809-555-5678"
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Chequeo de rutina",
            Status = "Pending",
            RequestedAt = DateTime.Now
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score >= 40, $"Score should be >= 40 for senior patient, got {score}");
    }

    [Fact]
    public void CalculateScore_PatientWithMedicalConditions_AddsBonusPoints()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Juan",
            LastName = "Perez",
            DateOfBirth = new DateTime(1990, 5, 15),
            ContactNum = "809-555-1234",
            MedicalConditions = new List<MedicalCondition>
            {
                new MedicalCondition { ConditionID = 1, ConditionName = "Diabetes" }
            }
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Chequeo de rutina",
            Status = "Pending",
            RequestedAt = DateTime.Now
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score >= 30, $"Score should be >= 30 with medical conditions, got {score}");
    }

    [Fact]
    public void CalculateScore_WaitingMoreThan5Days_AddsBonusPoints()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Juan",
            LastName = "Perez",
            DateOfBirth = new DateTime(1990, 5, 15),
            ContactNum = "809-555-1234"
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Chequeo de rutina",
            Status = "Pending",
            RequestedAt = DateTime.Now.AddDays(-10)
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score >= 30, $"Score should be >= 30 for waiting > 5 days, got {score}");
    }

    [Fact]
    public void CalculateScore_Waiting2To5Days_AddsMediumBonus()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Juan",
            LastName = "Perez",
            DateOfBirth = new DateTime(1990, 5, 15),
            ContactNum = "809-555-1234"
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Chequeo de rutina",
            Status = "Pending",
            RequestedAt = DateTime.Now.AddDays(-3)
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score >= 20, $"Score should be >= 20 for waiting 2-5 days, got {score}");
    }

    [Fact]
    public void GetLevel_Score100_ReturnsAlta()
    {
        var service = new PriorityService();
        var level = service.GetLevel(100);

        Assert.Equal("Alta", level);
    }

    [Fact]
    public void GetLevel_Score60_ReturnsMedia()
    {
        var service = new PriorityService();
        var level = service.GetLevel(60);

        Assert.Equal("Media", level);
    }

    [Fact]
    public void GetLevel_Score59_ReturnsBaja()
    {
        var service = new PriorityService();
        var level = service.GetLevel(59);

        Assert.Equal("Baja", level);
    }

    [Fact]
    public void CalculateScore_CompleteScenario_CriticalPatient_HighScore()
    {
        var patient = new Patient
        {
            PatientID = 1,
            FirstName = "Roberto",
            LastName = "Martinez",
            DateOfBirth = new DateTime(1950, 8, 20),
            ContactNum = "809-555-9999",
            MedicalConditions = new List<MedicalCondition>
            {
                new MedicalCondition { ConditionID = 1, ConditionName = "Corazón" },
                new MedicalCondition { ConditionID = 2, ConditionName = "Presión alta" }
            }
        };

        var appointment = new Appointment
        {
            AppointmentID = 1,
            PatientID = 1,
            Patient = patient,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Emergencia",
            Status = "Pending",
            RequestedAt = DateTime.Now.AddDays(-8)
        };

        var service = new PriorityService();
        var score = service.CalculateScore(appointment);

        Assert.True(score >= 100, $"Critical patient should have score >= 100, got {score}");
    }
}