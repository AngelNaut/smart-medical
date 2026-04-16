using SmartMedical.Application.DTOs.Appointment;

namespace SmartMedical.Application.Interfaces;

public interface IAppointmentService
{
    Task<AppointmentResponseDto> RegisterAppointmentAsync(CreateAppointmentDto dto);
    Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync();
    Task<AppointmentResponseDto?> GetAppointmentByIdAsync(int id);
    Task UpdateAppointmentAsync(int id, UpdateAppointmentDto dto);
    Task DeleteAppointmentAsync(int id);
}
