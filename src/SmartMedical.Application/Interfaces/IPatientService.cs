using SmartMedical.Application.DTOs.Patient;

namespace SmartMedical.Application.Interfaces;

public interface IPatientService
{
    Task<PatientResponseDto> RegisterPatientAsync(CreatePatientDto dto);
    Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync();
    Task<PatientResponseDto?> GetPatientByIdAsync(int id);
    Task UpdatePatientAsync(int id, UpdatePatientDto dto);
    Task DeletePatientAsync(int id);
}
