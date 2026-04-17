namespace SmartMedical.Domain.Entities;

public class Patient
{
    public int PatientID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? ContactNum { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<MedicalCondition> MedicalConditions { get; set; } = new List<MedicalCondition>();
}
