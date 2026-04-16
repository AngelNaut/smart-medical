namespace SmartMedical.Domain.Entities;

public class MedicalCondition
{
    public int ConditionID { get; set; }
    public int PatientID { get; set; }
    public string ConditionName { get; set; } = string.Empty;
    public DateTime? DiagnosisDate { get; set; }

    public Patient Patient { get; set; } = null!;
}
