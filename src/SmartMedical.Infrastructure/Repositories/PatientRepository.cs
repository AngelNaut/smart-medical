using Microsoft.EntityFrameworkCore;
using SmartMedical.Domain.Entities;
using SmartMedical.Domain.Interfaces;
using SmartMedical.Infrastructure.Persistence;

namespace SmartMedical.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly SmartMedicalDbContext _context;

    public PatientRepository(SmartMedicalDbContext context)
    {
        _context = context;
    }

    public async Task<Patient> AddAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}
