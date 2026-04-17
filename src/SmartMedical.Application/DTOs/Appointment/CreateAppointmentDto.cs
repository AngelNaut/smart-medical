namespace SmartMedical.Application.DTOs.Appointment;

public class CreateAppointmentDto
{
    public int PatientID { get; set; }
    public DateTime DateTime { get; set; }
    public string UrgencyDescription { get; set; } = string.Empty;
}
