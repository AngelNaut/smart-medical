using SmartMedical.Application.DTOs.Appointment;
using SmartMedical.Application.Interfaces;
using SmartMedical.Application.Services;
using SmartMedical.Domain.Interfaces;
using SmartMedical.Infrastructure.Repositories;
using SmartMedical.Infrastructure.Persistence;

namespace SmartMedical.Test;

public class AppointmentTests
{
    [Fact]
    public async Task RegisterAppointmentAsync_ValidData_ReturnsCreatedAppointment()
    {
        var context = TestDbContext.CreateWithData();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        var dto = new CreateAppointmentDto
        {
            PatientID = 1,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "High"
        };

        var result = await service.RegisterAppointmentAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(1, result.PatientID);
        Assert.Equal("High", result.UrgencyDescription);
        Assert.Equal("Pending", result.Status);
    }

    [Fact]
    public async Task RegisterAppointmentAsync_PatientNotFound_ThrowsException()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        var dto = new CreateAppointmentDto
        {
            PatientID = 999,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "High"
        };

        await Assert.ThrowsAsync<Exception>(() => service.RegisterAppointmentAsync(dto));
    }

    [Fact]
    public async Task GetAllAppointmentsAsync_ReturnsAllAppointments()
    {
        var context = TestDbContext.CreateWithData();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        await service.RegisterAppointmentAsync(new CreateAppointmentDto
        {
            PatientID = 1,
            DateTime = DateTime.Now.AddDays(3),
            UrgencyDescription = "High"
        });

        await service.RegisterAppointmentAsync(new CreateAppointmentDto
        {
            PatientID = 1,
            DateTime = DateTime.Now.AddDays(7),
            UrgencyDescription = "Low"
        });

        var result = await service.GetAllAppointmentsAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ReviewAndScheduleAppointmentAsync_ValidAppointment_ChangesStatusToScheduled()
    {
        var context = TestDbContext.CreateWithData();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        var appointment = await service.RegisterAppointmentAsync(new CreateAppointmentDto
        {
            PatientID = 1,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "High"
        });

        var scheduledDate = DateTime.Now.AddDays(10);
        var result = await service.ReviewAndScheduleAppointmentAsync(appointment.AppointmentID, scheduledDate);

        Assert.NotNull(result);
        Assert.Equal("Scheduled", result.Status);
    }

    [Fact]
    public async Task ReviewAndScheduleAppointmentAsync_InvalidAppointmentId_ThrowsException()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        await Assert.ThrowsAsync<Exception>(() => service.ReviewAndScheduleAppointmentAsync(999, DateTime.Now.AddDays(10)));
    }

    [Fact]
    public async Task ReviewAndScheduleAppointmentAsync_DefaultDateTime_ThrowsException()
    {
        var context = TestDbContext.CreateWithData();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        var appointment = await service.RegisterAppointmentAsync(new CreateAppointmentDto
        {
            PatientID = 1,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "High"
        });

        await Assert.ThrowsAsync<Exception>(() => service.ReviewAndScheduleAppointmentAsync(appointment.AppointmentID, default));
    }

    [Fact]
    public async Task GetAppointmentByIdAsync_ExistingAppointment_ReturnsAppointment()
    {
        var context = TestDbContext.CreateWithData();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        var created = await service.RegisterAppointmentAsync(new CreateAppointmentDto
        {
            PatientID = 1,
            DateTime = DateTime.Now.AddDays(5),
            UrgencyDescription = "Medium"
        });

        var result = await service.GetAppointmentByIdAsync(created.AppointmentID);

        Assert.NotNull(result);
        Assert.Equal("Medium", result.UrgencyDescription);
    }

    [Fact]
    public async Task GetAppointmentByIdAsync_NonExistingAppointment_ReturnsNull()
    {
        var context = TestDbContext.CreateInMemoryContext();
        IAppointmentRepository appointmentRepo = new AppointmentRepository(context);
        IPatientRepository patientRepo = new PatientRepository(context);

        var service = new AppointmentService(appointmentRepo, patientRepo);

        var result = await service.GetAppointmentByIdAsync(999);

        Assert.Null(result);
    }
}