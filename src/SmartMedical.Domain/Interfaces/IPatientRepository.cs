using SmartMedical.Domain.Entities;

namespace SmartMedical.Domain.Interfaces;

public interface IPatientRepository
{
    Task<Patient> AddAsync(Patient patient);
    Task<Patient?> GetByIdAsync(int id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(int id);
}
