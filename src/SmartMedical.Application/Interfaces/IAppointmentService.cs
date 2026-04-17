using SmartMedical.Application.DTOs.Appointment;

namespace SmartMedical.Application.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResponseDto> RegisterAppointmentAsync(CreateAppointmentDto dto);
    Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync(string? status = null, string? urgency = null);
    Task<AppointmentResponseDto?> GetAppointmentByIdAsync(int id);
    Task<AppointmentResponseDto> ReviewAndScheduleAppointmentAsync(int id, DateTime scheduledDateTime);
    Task UpdateAppointmentAsync(int id, UpdateAppointmentDto dto);
    Task DeleteAppointmentAsync(int id);
}
