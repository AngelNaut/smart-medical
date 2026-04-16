using SmartMedical.Application.DTOs.Patient;
using SmartMedical.Application.Interfaces;
using SmartMedical.Domain.Entities;
using SmartMedical.Domain.Interfaces;

namespace SmartMedical.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientResponseDto> RegisterPatientAsync(CreatePatientDto dto)
    {
        var patient = new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            ContactNum = dto.ContactNum
        };

        var createdPatient = await _patientRepository.AddAsync(patient);

        return MapToDto(createdPatient);
    }

    public async Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        return patients.Select(MapToDto);
    }

    public async Task<PatientResponseDto?> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null) return null;

        return MapToDto(patient);
    }

    public async Task UpdatePatientAsync(int id, UpdatePatientDto dto)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
            throw new Exception("Patient not found");

        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.ContactNum = dto.ContactNum;

        await _patientRepository.UpdateAsync(patient);
    }

    public async Task DeletePatientAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
            throw new Exception("Patient not found");

        await _patientRepository.DeleteAsync(id);
    }

    private PatientResponseDto MapToDto(Patient patient)
    {
        return new PatientResponseDto
        {
            PatientID = patient.PatientID,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            ContactNum = patient.ContactNum
        };
    }
}
