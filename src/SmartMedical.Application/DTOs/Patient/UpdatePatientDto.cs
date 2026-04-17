namespace SmartMedical.Application.DTOs.Patient;

public class UpdatePatientDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? ContactNum { get; set; }
}
