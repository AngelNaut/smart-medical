using SmartMedical.Domain.Entities;

namespace SmartMedical.Domain.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment> AddAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(int id);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<bool> ReviewAndScheduleAsync(int id, DateTime scheduledDateTime);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(int id);
}
