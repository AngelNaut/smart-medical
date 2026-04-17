using Microsoft.EntityFrameworkCore;
using SmartMedical.Domain.Entities;
using SmartMedical.Domain.Interfaces;
using SmartMedical.Infrastructure.Persistence;

namespace SmartMedical.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly SmartMedicalDbContext _context;

    public AppointmentRepository(SmartMedicalDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
                             .Include(a => a.Patient)
                             .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments
                             .Include(a => a.Patient)
                             .FirstOrDefaultAsync(a => a.AppointmentID == id);
    }

    public async Task<bool> ReviewAndScheduleAsync(int id, DateTime scheduledDateTime)
    {
        var affectedRows = await _context.Appointments
            .Where(a => a.AppointmentID == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(a => a.DateTime, scheduledDateTime)
                .SetProperty(a => a.Status, "Scheduled"));

        return affectedRows > 0;
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
