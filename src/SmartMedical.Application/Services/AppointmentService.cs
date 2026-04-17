using SmartMedical.Application.DTOs.Appointment;
using SmartMedical.Application.Interfaces;
using SmartMedical.Domain.Entities;
using SmartMedical.Domain.Interfaces;

namespace SmartMedical.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
    }

    public async Task<AppointmentResponseDto> RegisterAppointmentAsync(CreateAppointmentDto dto)
    {
        var patient = await _patientRepository.GetByIdAsync(dto.PatientID);
        if (patient == null)
            throw new Exception("Patient not found");

        var appointment = new Appointment
        {
            PatientID = dto.PatientID,
            DateTime = dto.DateTime,
            UrgencyDescription = dto.UrgencyDescription,
            Status = "Pending",
            RequestedAt = DateTime.UtcNow
        };

        var createdAppointment = await _appointmentRepository.AddAsync(appointment);
        
        createdAppointment.Patient = patient;

        return MapToDto(createdAppointment);
    }

    public async Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync()
    {
        var appointments = await _appointmentRepository.GetAllAsync();
        return appointments.Select(MapToDto);
    }

    public async Task<AppointmentResponseDto?> GetAppointmentByIdAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null) return null;

        return MapToDto(appointment);
    }

    public async Task<AppointmentResponseDto> ReviewAndScheduleAppointmentAsync(int id, DateTime scheduledDateTime)
    {
        if (scheduledDateTime == default)
            throw new Exception("Scheduled date is required");

        var wasUpdated = await _appointmentRepository.ReviewAndScheduleAsync(id, scheduledDateTime);
        if (!wasUpdated)
            throw new Exception("Appointment not found");

        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
            throw new Exception("Appointment not found");

        return MapToDto(appointment);
    }

    public async Task UpdateAppointmentAsync(int id, UpdateAppointmentDto dto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
            throw new Exception("Appointment not found");

        appointment.DateTime = dto.DateTime;
        appointment.Status = dto.Status;
        appointment.UrgencyDescription = dto.UrgencyDescription;

        await _appointmentRepository.UpdateAsync(appointment);
    }

    public async Task DeleteAppointmentAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
            throw new Exception("Appointment not found");

        await _appointmentRepository.DeleteAsync(id);
    }

    private AppointmentResponseDto MapToDto(Appointment appointment)
    {
        return new AppointmentResponseDto
        {
            AppointmentID = appointment.AppointmentID,
            PatientID = appointment.PatientID,
            PatientName = appointment.Patient != null ? $"{appointment.Patient.FirstName} {appointment.Patient.LastName}" : "Unknown",
            DateTime = appointment.DateTime,
            Status = appointment.Status,
            UrgencyDescription = appointment.UrgencyDescription,
            RequestedAt = appointment.RequestedAt
        };
    }
}
