namespace SmartMedical.Application.DTOs.Patient;

public class PatientResponseDto
{
    public int PatientID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? ContactNum { get; set; }
}
