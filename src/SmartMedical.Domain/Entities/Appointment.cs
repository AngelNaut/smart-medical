namespace SmartMedical.Domain.Entities;

public class Appointment
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public DateTime DateTime { get; set; }
    public string Status { get; set; } = "Pending";
    public string UrgencyDescription { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; } = DateTime.Now;

    public Patient Patient { get; set; } = null!;
    public PriorityScore? PriorityScore { get; set; }
}
