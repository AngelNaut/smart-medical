namespace SmartMedical.Application.DTOs.Appointment;

public class AppointmentResponseDto
{
    public int AppointmentID { get; set; }
    public int PatientID { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public string UrgencyDescription { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; }
    public int PriorityScore { get; set; }
    public string PriorityLevel { get; set; } = string.Empty;
}
